Seja o que for, tenha um gerenciador de configuração para você.
Use variáveis de ambiente ou parâmetros de chamada.

No caso do aspnet, temos a opção do ConfigurationManager. Entretanto, esse não 
é um mecanismo exclusivo do ASPNET e pode ser usado em programas de linha de comando.

Repositório Chave/Valor
=========================

Começamos incluindo as dependências no project.json:

```
  "dependencies": {
    "Microsoft.Extensions.Configuration": "1.0.0",
    "Microsoft.Extensions.Configuration.CommandLine": "1.0.0",
    "Microsoft.Extensions.Configuration.EnvironmentVariables": "1.0.0",
    "Microsoft.Extensions.Configuration.UserSecrets": "1.0.0"
  },
```

O cenario simples é receber parâmetros via linha de comando ou através
de variáveis de ambiente. 

```
    var builder = new ConfigurationBuilder()
        .AddCommandLine(args)
        .AddEnvironmentVariables();
```

usando:

```
    var config = builder.Build();
    string mensagem = config["hello"];

    Console.WriteLine(mensagem);
```

Por exemplo, posso chamar o programa:

    dstress --msg "hello world"

    set msg=hello world
    dstress

Podemos inclusive tornar as variaveis de ambiente mais customizadas para
nosso aplicativo usando um prefixo.

    .AddEnvironmentVariables("DSTRESS_");
    
Assim, podemos usar a variável de ambiente DSTRESS_MSG e evitar conflito de nomes.


# config.json

Uma opção interessante é usar arquivos de configuração. Podemos adicionar o suporte
a arquivos com extensão JSON ou XML. No exemplo abaixo, colocamos um arquivo config.json
com o formato:

```
    {
        "msg": "hello from Json"
    }
```
 
 Para isso basta incluir a linha:

    .AddJsonFile("config.json", optional: true);

Se você quiser testar, basta copiar o arquivo config.json para a pasta do executável
e tudo funcionará.

Existe uma outra situação que é quando precisamos incluir obrigatoriamente um arquivo
de configuração. Por exemplo, um arquivo config.default.json.

```
    var builder = new ConfigurationBuilder()
        .AddJsonFile("config.default.json");
        .AddCommandLine(args)
        .AddEnvironmentVariables();
```

Note que duas coisas que ocorrem:
1. A ordem de chamada importa - as configurações prevalecem na ordem.
2. O arquivo `config.default.json` é obrigatório, pois não há o parâmetro `optional`.

Ao adicionar o arquivo no projeto, precisamos incluir no build:

```
  "buildOptions": {
    ...
    "copyToOutput": [ "config.default.json" ]
  },
```


User Secrets

Erro muito comum é armazenar senhas e chaves privadas no Git.

secrets.json

%APPDATA%\microsoft\UserSecrets\<userSecretsId>\secrets.json 

project.json

    "userSecretsId": "aspnet-dstress-20160702094745"

Aparentemente podemos usar o seguinte comando:

    builder.AddUserSecrets();

copiar o arquivo project.json para o diretório.

embora o mais fácil seja incluir a linha de comando: 

    builder.AddUserSecrets("aspnet-dstress-20160702094745");

https://docs.asp.net/en/latest/security/app-secrets.html


GetSection
------------

se voce procurar por tutoriais na internet encontrará muitos falando 
sobre configurações do tipo:

abc:def:etc

auth:facebook:appid
data:sql:connstr

ConfigurationRoot e 