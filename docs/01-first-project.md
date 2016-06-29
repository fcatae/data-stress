A criação do primeiro projeto
==============================

Criar um projeto no Visual Studio é fácil por conta dos templates. O arquivo 
no formato JSON possui informações básicas sobre o projeto (nome, versão, autor,
sumário, descrição, licença de uso, URL do projeto, etc).

É possível definir diferentes opções de compilação e build usando as opções
de `Configuration` e `Frameworks`. 

Por exemplo, podemos mudar a compilação de acordo com o tipo de build (Debug ou Release): 

```
"configurations": {
    "Debug": {
    "compilationOptions": {
        "define": ["DEBUG", "TRACE"]
    }
    },
    "Release": {
    "compilationOptions": {
        "define": ["RELEASE", "TRACE"],
        "optimize": true
    }
}
```

Assim como é possível selecionar diferentes dependências de acordo com a plataforma, que
são identificados pelos Target Framework Moniker (TFM).

```
    "frameworks": {
        "net452": {},
        "netcoreapp1.0": {
            "dependencies": {
                ...
            }
    }
```

O arquivo `project.json` contém a seção `framework` para indicar as diferentes plataformas
na qual o aplicativo está disponível. Entretanto, como o termo 'plataforma' tem sido 
usado de forma exagerada, podemos ser mais precisos ao dizer Target Framework Monitor (TFM).
Aqui estamos falando das aplicações baseadas no .NET Framework (Desktop) ou .NET Core 
(cross-plataforma), assim como do Universal Windows Platform (UWP), Windows Phone Silverlight
e Xamarin.

Por isso, ao criar um aplicativo .NET Core, temos a definição da plataforma `netcoreapp1.0` 
com a dependência dos binários `Microsoft.NETCore.App`.

```
    {
        "version": "1.0.0-*",
        "buildOptions": {
            "emitEntryPoint": true
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
            "net452": { }
        }
    }
```

Visual Studio realiza o build de todas as plataformas selecionadas, além de permitir selecionar 
o framework a ser executado durante a depuração.

Assuntos relacionados: Target Framework Moniker (TFM), imports, Netstandards e NetcoreApp.
