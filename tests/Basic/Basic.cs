using CppSharp.AST;
using CppSharp.Generators;
using CppSharp.Passes;
using CppSharp.Utils;

namespace CppSharp.Tests
{
    public class Basic : GeneratorTest
    {
        public Basic(GeneratorKind kind)
            : base("Basic", kind)
        {

        }

        public override void Setup(Driver driver)
        {
            base.Setup(driver);

            driver.Options.OutputNamespace = "BasicTest";
        }

        public override void SetupPasses(Driver driver)
        {
            driver.Options.GenerateCopyConstructors = true;
            driver.Options.MarshalCharAsManagedChar = true;
            driver.Options.GenerateProperties = true;
            driver.Options.GenerateConversionOperators = true;
        }

        public override void Preprocess(Driver driver, ASTContext ctx)
        {
            driver.AddTranslationUnitPass(new GetterSetterToPropertyPass());
            driver.AddTranslationUnitPass(new CheckMacroPass());
            ctx.SetClassAsValueType("Bar");
            ctx.SetClassAsValueType("Bar2");
            ctx.IgnoreClassWithName("IgnoredType");
        }

        public static void Main(string[] args)
        {
            ConsoleDriver.Run(new Basic(GeneratorKind.CLI));
            ConsoleDriver.Run(new Basic(GeneratorKind.CSharp));
        }
    }
}
