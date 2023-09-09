using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace BitN.SourceGen;

[Generator]
public class BitNGenerator : IIncrementalGenerator
{
    private const string REFERENCE_PATH = @"..\..\..\BitN\BitNRef.cs";

    public void Initialize(IncrementalGeneratorInitializationContext context)
        => context.RegisterPostInitializationOutput(PostInitCallBack);

    private static void PostInitCallBack(IncrementalGeneratorPostInitializationContext ctx)
    {
        try
        {
            ReadOnlySpan<string> bitNRefSrc = GetSourceCode(REFERENCE_PATH).AsSpan();

            //byte backing
            for (int i = 1; i < 8; i++)
                ctx.AddSource($"Bit{i}.g.cs", SourceText.From(BuildRefSource(bitNRefSrc, "byte", 1, i), Encoding.UTF8));
            //ushort backing
            for (int i = 9; i < 16; i++)
                ctx.AddSource($"Bit{i}.g.cs", SourceText.From(BuildRefSource(bitNRefSrc, "ushort", 2, i), Encoding.UTF8));
            //uint backing
            for (int i = 17; i < 32; i++)
                ctx.AddSource($"Bit{i}.g.cs", SourceText.From(BuildRefSource(bitNRefSrc, "uint", 4, i), Encoding.UTF8));
        }
        //In case source gen fails for any reason, gen a single file with the exception
        catch (Exception e)
        {
            ctx.AddSource($"exception.g.cs", SourceText.From(e.ToString(), Encoding.UTF8));
        }
    }

    private static string[] GetSourceCode(string filepath, [CallerFilePath] string root = "")
        => File.ReadAllLines(root + filepath);

    private static string BuildRefSource(ReadOnlySpan<string> refSrc, string type, int byteSize, int N)
    {
        StringBuilder sb = new();
        bool inRefComments = false;
        foreach (string line in refSrc)
        {
            string newLine = line;

            //ignore template comments
            if (line.Contains("$startrefcomments")) inRefComments = true;
            if (line.Contains("$endrefcomments"))
            {
                inRefComments = false;
                continue;
            }
            if (inRefComments) continue;
            
            //$N -> Replace 5 w/ N
            if (line.Contains("$N")) newLine = newLine.Replace("5", $"{N}");
            //$type -> replace byte w/ backing type
            if (line.Contains("$type")) newLine = newLine.Replace("byte", $"{type}");
            //$makepublic -> replace all "internal" w/ "public"
            if (line.Contains("$makepublic")) newLine = newLine.Replace("internal", "public");
            //$max -> replace 31 w/ 2^N-1
            if (line.Contains("$max")) newLine = newLine.Replace("31", $"{(1UL << N) - 1}");
            //$1byte -> ? do nothing : delete line
            if (line.Contains("$1byte") && byteSize != 1) continue;
            //$1or2byte -> ? do nothing : delete line
            if (line.Contains("$1or2byte") && byteSize > 2) continue;
            //$4byte -> ? replace first two chars with "  " : delete line
            if (line.Contains("$4byte"))
            {
                if (byteSize == 4)
                    newLine = "  " + newLine.Substring(2);
                else continue;
            }

            //Remove everything that follows "//$"
            int cmdStart = newLine.IndexOf("//$");
            if (cmdStart != -1) newLine = newLine.Substring(0, cmdStart);

            sb.AppendLine(newLine.ToString()); //add processed line to src file
        }
        //Replace all BitNRef w/ "Bit{N}"
        sb.Replace("BitNRef", $"Bit{N}");

        return sb.ToString();
    }
}
