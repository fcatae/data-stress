Test Driven Development (TDD)
===============================

Até o momento, definimos uma interface, implementamos uma classe e falta testar.  

```
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("DSTRESS");

            CheckDataDriver(new InMemoryDataDriver());
        }

        static void CheckDataDriver(IDataDriver driver)
        {
            string id = driver.Create(new { name = "Fabricio" });
            object original = driver.Read(id);

            bool wasUpdated = driver.Update(id, new { height = 10, width = 10 });

            object modified = driver.Read(id);

            bool wasDeleted = driver.Delete(id);
        }
    }
```

Esse é o fluxo principal de execução: create, read, update, delete. Mas certamente
existem casos interessantes que precisamos saber como o programa se comporta.

Por exemplo:
* O que acontece se passar um `ID` inválido para `Delete()`?
* O que acontece se passar um valor `null` como modelo do `Update()`?
* O que acontece se chamar o método `Init()` mais de uma vez?

Como fazer para testar exaustivamente que todo o fluxo? 


# Projeto de Teste