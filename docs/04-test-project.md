Test Project
==============

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

## Passo 1: Criar um projeto para teste

O projeto é uma mistura entre Application e Library: o projeto segue um template de 
aplicativo .NET Core, embora não existe um entry point implementado no nosso programa.
Na realidade, o método `Main()` será implementado na biblioteca do xUnit.

No arquivo project.json, precisamos adicionar uma linha de referência ao xUnit. 

```
  "testRunner": "xunit",
```

Esse comando indica que os testes rodaram a linha de comando: dotnet-test-xunit.

## Passo 2: Adicionar a dependência do xUnit

Em seguida, adicionamos a referência ao `xunit` com versão >= 2.2. Essa biblioteca 
implementa o suporte a teste e a linha de comando.

```
  "dependencies": {
    "xunit": "2.2.0-*",
    "dotnet-test-xunit": "2.2.0-*"
  },
```

Não é necessário adicionar referência a "xunit.runner.visualstudio".


## Passo 3: Adicionar os frameworks .NET Core e/ou Full

Podemos adicionar suporte para testes nos diferentes frameworks. Adicionaremos 
a versão 4.5.2 (dotnet-test-xunit requer >= 4.5.1). 

```
  "frameworks": {

    "netcoreapp1.0": { ... }
    "net452": { }
```

Quando ambos os frameworks são referenciados, o Visual Studio - Test Manager não
funciona corretamente e retorna um erro:

    Discovering tests in 'C:\Users\fabricio\Desktop\data-stress\test\Test-DataDriver\project.json' ["C:\Program Files\dotnet\dotnet.exe" test "C:\Users\fabricio\Desktop\data-stress\test\Test-DataDriver\project.json" --output "C:\Users\fabricio\Desktop\data-stress\test\Test-DataDriver\bin\Debug\net452\win7-x64" --port 59805 --parentProcessId 3676 --no-build]
    Unable to start C:\Program Files\dotnet\dotnet.exe
    dotnet-test Error: 0 : [ReportingChannel]: Waiting for message failed System.IO.IOException: Unable to read data from the transport connection: An established connection was aborted by the software in your host machine. ---> System.Net.Sockets.SocketException: An established connection was aborted by the software in your host machine

Esse erro é causado porque dois processos tentam abrir a mesma porta TCP/IP.


## (Bug: opcional): Solução de contorno para o bug System.Runtime

Em algumas condições, o sistema gera erro de referência ao assembly do Runtime (PCL).

```
    "net452": {
      "frameworkAssemblies": {
        "System.Runtime": "4.0.0.0"
      }
    }
```


# Testes Unitários

Para usar os testes unitários basta referenciar o `namespace Xunit` e usar o 
atributo `[Fact]` aos métodos.

```
using Xunit;

public class Teste
{
    [Fact]
    void Somar()
    {
        Assert.Equal( 1 + 1 , 2 );
    }
}
```


Referência
------------

# project.json

```
    {
        "version": "1.0.0-*",
        "testRunner": "xunit",

        "dependencies": {
            "xunit": "2.2.0-*",
            "dotnet-test-xunit": "2.2.0-*"
        },

        "frameworks": {

            "netcoreapp1.0": {
                "dependencies": {
                    "Microsoft.NETCore.App": {
                    "type": "platform",
                    "version": "1.0.0"
                    }
                },
                "imports": "dnxcore50"
            },

            "net45": {
                "frameworkAssemblies": {
                    "System.Runtime": "4.0.0.0"
                }
            }

        }
    }
```