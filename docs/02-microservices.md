Arquitetura
============

Antes de iniciar o projeto, é importante definir alguns dos objetivos a serem alcançados.
Isso inclui definir uma arquitetura e as tecnologias mais adequadas para atender à
visão proposta. 

# Planejamento 

O planejamento foi realizado em 3 etapas: requisitos de projeto, decisão de tecnologia e
componentes de sistema.

## Parte 1: Requisitos de Projeto

Quando começamos o projeto, queríamos uma aplicação bem desacoplada para evitar dependências
complexas. O objetivo era desenhar uma aplicação com escalabilidade e disponibilidade. 
Por isso, era necessário adotar uma arquitetura moderna:

    Como requisito de projeto, a arquitetura seria de Microserviços.

Entretanto, essa afirmação não diz muita coisa. Em geral, observamos que o termo 'microserviços' 
tem sido explorado amplamente sem uma definição concreta. Podemos dizer que os microserviços 
são autônomos e rodam de forma independente. Além disso, não existe uma imposição sobre a
tecnologia.

## Parte 2: Decisão de Tecnologia

Ao falar sobre microserviços, há uma tendência natural ao uso de:
* REST API
* Containers
* Store NoSQL

A partir de então, várias tecnologias próximas se somam a discussão e os requisitos aumentam
sem que percebemos. Podemos começar a falar sobre clusters de containers (orquestração), 
processo de autenticação OAUTH entre serviços, extração de dados usando map-reduce ou Hadoop. 
As discussões se aprofundam nos detalhes e, ao final, temos uma lista de requisitos de
projeto sem ter uma arquitetura definida.

A grosso modo, poderíamos resolver esse problema com um desenho simples com os componentes do
sistema. Por isso, partimos para identificar os subprojetos.

## Parte 3: Componentes do Sistema

Identificamos que o sistema poderia ser quebrado em vários componentes menores e independentes
entre si. Cada componente teria sua responsabilidade e com autonomia para rodar. O mais importante
é que o repositório de dados (normalmente um banco de dados relacional) não seria compartilhado
entre eles.

Ao contrário de um sistema monolítico, os dados ficam armazenados em diferentes repositórios
e não podem ser acessados diretamente. O correto é que o acesso seja feito pela API disponibilizada
pelo microserviço. Se não estamos usando os recursos de um banco de dados tradicional, então 
podemos usar um repositório NoSQL para tentar diminuir o custo.

Uma camada de frontend (outro microserviço) faria a agregação de chamada aos demais microserviços
usando o protocolo HTTP.  

# Resultados

Infelizmente ao longo do projeto compreendemos que erramos durante o planejamento. Embora não
seja possível voltar ao passado para provar diferentes alternativas, compartilho a minha opinião
sobre os nossos possíveis erros:

1. Requisitos de Projeto: Começamos sem decidir a arquitetura (Microserviço não é arquitetura). 

    Definir uma arquitetura com base no fluxo e armazenamento de dados. Por exemplo, poderíamos
    falar sobre cliente/servidor, uso de filas (eventos) ou barramento de mensagem.     

2. Decisão de Tecnologia: Escolhemos as tecnologias sem conhecer a arquitetura do sistema.

    Tecnologia deve ser escolhida com base na arquitetura desenhada e nos requisitos do sistema.
    Devemos incluir os cenários de desenvolvimento (máquina local) e produção (cloud).

3. Componentes do Sistema: Aumentamos a complexidade do sistema no início do desenvolvimento.

    Adotar um sistema monolítico evita a necessidade de se preocupar em comunicação entre serviços
    (CORS, OAUTH, serialização de dados, etc) e simplifica as alterações de projeto (ex: mudança
    de tipo de dados, quebra de interfaces).


# Microserviços x Monolítico

Se você estiver começando um projeto, então inicie pensando em monolítico.

- Comece com um único repositório GIT
- Crie um roadmap de funcionalidades
- Inicie uma sprint para gerar os primeiros resultados
- Incorpore graduamente novas tecnologias 

Conforme o projeto se desenvolva, quebre o projeto em subprojetos menores com responsabilidades
bem definidas. A partir desse momento, pense microserviços.


Assuntos Relacionados: Multi-tenant

