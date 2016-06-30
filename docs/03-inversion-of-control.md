Inversion of Control
======================

Dizem que a arquitetura define as tecnologias empregadas, assim como informações
sobre a distribuição (ex: topologia de rede, servidores, etc). Uma boa arquitetura
identifica os componentes do sistema e suas responsabilidades.

É comum adotar frameworks que encapsulam as funcionalidades principais do sistema,
sendo necessário desenvolver somente as partes customizáveis. Esse é o conceito 
conhecido como Inversion of Control (IoC).

Exemplo:  

```
    void main()
    {
        // Cria uma janela azul
        CreateBlueWindow();
    }
```

Quando falamos sobre IoC (inversão de controle), estamos escondendo os detalhes
de implementação no código principal.

```
    void main()
    {
        // Cria uma janela
        Framework.CreateWindow();
    }
```

Isso é semelhante a pensar em um framework conectado a plugins. Diferentes plugins
podem ser conectados ao framework.

```
    Framework.Window = new BlueWindow();
```

Essa flexibilidade é fundamental para modularizar os sistemas.


# Interface x Implementação

A programação orientada a objeto facilita a tarefa de separar a definição e a
implementação do objeto. A ideia é usar `interface` como contratos entre componentes. 

O exemplo abaixo mostra que existe um contrato (`interface IDataDriver`) que garante
a existência do método `Create()`, tornando a sintaxe válida para o compilador
(tipagem estática). 

```
IDataDriver database = ???
database.Create();
```

Por outro lado, não sabemos os detalhes sobre a implementação do método: qual o tipo
de banco de dados? A separação entre definição (interface) e implementação (classe)
permite usar diferentes 

```
IDataDriver database = new SQLDataDriver();
IDataDriver database = new InMemoryDataDriver();
IDataDriver database = new ExcelDataDriver();
```


## Definição da Interface

A `interface` fornece uma especificação sobre os métodos e propriedades presentes
em um objeto. 

```
    public interface IDataDriver
    {
        void Init();
        string Create(object model);
        object Read(string id);
        bool Update(string id, object model);
        bool Delete(string id);
    }
```

## Implementação da Classe

A implementação é feita ao implementar a `interface`.

```
    public class SQLDataDriver : IDataDriver
    {
        public string Create(object model)
        {
            ...
        }

        public bool Delete(string id)
        {
            ...
        }

        public void Init()
        {
            ...
        }

        public object Read(string id)
        {
            ...
        }

        public bool Update(string id, object model)
        {
            ...
        }
    }
```

# Técnicas de Inversion of Control

Embora as interfaces facilitem o desacoplamento de componentes, elas sozinhas 
não resolvem o problema completamente. 

Por exemplo, a classe `Teste` se conecta a um banco de dados SQL. Se quisermos
mudar a lógica para conectar a um repositório Excel, então seria necessário
mudar o código e compilar a classe novamente. 

```
    class Teste()
    {
        void Init() 
        {        
            IDataDriver database = new SQLDataDriver();
            database.Create();
        }
    }
```

Existem algumas estratégias conhecidas para esconder a implementação.

## Service Locator

Service Locator registra os componentes durante o momento inicial do programa, 
associando uma interface à implementação. 

```
    ServiceLocator.RegisterDataDriver(new SQLDataDriver);
```

Assim, podemos usar a seguinte implementação para a classe `Teste`.

```
    void Init() 
    {        
        IDataDriver database = ServiceLocator.GetDataDriver();
        database.Create();
    }
```


## Factory

Factory esconde a implementação do construtor de um determinado objeto através
de uma classe 'factory'.     

```
    void Init(IDataDriverFactory factory) 
    {        
        IDataDriver database = factory.CreateDataDriver();
        database.Create();
    }
```

Embora seja semelhante ao ServiceLocator, o uso desse padrão envolve a passagem
de 'factory' como parâmetro.


## Strategy Pattern

Strategy Pattern consiste em encapsular a lógica em um objeto e passar como parâmetro
ao método.    

 ```
    void Init(IDataDriver database) 
    {        
        database.Create();
    }
```

## Dependency Injection

A implementação mais comum de Dependency Injection é passar os objetos construídos
no construtor da classe.

 ```
    class Teste()
    {
        public Teste(IDataDriver database)
        {
            this._database = database;
        }

        void Init() 
        {        
            IDataDriver database = this._database;
            database.Create();
        }
    }
```

## Templates

Os templates é uma estratégia diferente de todas as outras e trabalha com tipagem
estática. Os templates abstraem os tipos de dados usados, definindo a classe `T`
implementação da interface `IDataDriver` e requer construtor (new).

 ```
    class TesteTemplate<T>() where T: IDataDriver, new()
    {
        void Init() 
        {        
            IDataDriver database = new T();
            database.Create();
        }
    }
```

Portanto, é possível definir posteriormente uma classe:

```
    class SQLTeste : TesteTemplate<SQLDataDriver>
    {
    }
```

Podemos empregar essa técnica para generalizar interfaces:

```
    public interface IDataDriver<T,TKey> 
    {
        void Init();
        TKey Create(T model);
        T Read(TKey id);
        bool Update(TKey id, T model);
        bool Delete(TKey id);
    }

    public interface IDataDriver : IDataDriver<object, string>
    {
    }
```
    