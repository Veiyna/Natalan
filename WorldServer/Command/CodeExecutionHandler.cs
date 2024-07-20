using System;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Shared.Command;
using Shared.Game;
using WorldServer.Network;

namespace WorldServer.Command;

public class CodeExecutionHandler
{
    [CommandHandler("eval", SecurityLevel.Console)]
    public static void HandleEval(WorldSession session, params string[] parameters)
    {
        var code = string.Join(" ", parameters);
        
        string parentNamespace = "WorldServer";
        
        var childNamespaces = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Select(t => t.Namespace)
            .Where(n => n != null && n.StartsWith(parentNamespace))
            .Distinct();
        
        var scriptOptions = ScriptOptions.Default
            .WithReferences(Assembly.GetExecutingAssembly()).WithImports("System").WithImports(childNamespaces);

        try
        {
            Console.WriteLine($"Returned {CSharpScript.EvaluateAsync(code, scriptOptions)}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
}