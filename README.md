# Backend Glicare APP

Glicare é um aplicativo dedicado a pacientes de diabetes, médicos, nutricionistas e demais profissionais da saúde que desejam maior simplicidade, praticidade e facilidade no acompanhamento, controle da evolução da doença e sintomas.

## Funcionalidades

- Autenticação
- Controle de glicemia
- Gerenciamento de usuário
- Feedbacks
- Configurações
- Notificações


## Stack utilizada

**Backend:** C# (ASP.Net 8)

**Banco de dados:** PostgreSQL, MongoDB

**Libs:**: MediaTR, FluentValidation, Dapper, SignalR

**Infra:**: GCP - Cloud Run (docker)

## Arquitetura proposta e estrutura do projeto

Utilizaremos CQRS com clean Architecture, mas afinal por quê?

CQRS (Command Query Responsibility Segregation) é um padrão arquitetural que separa as operações de leitura (queries) e escrita (commands) em modelos distintos, combinado perfeitamente com Clean Architecture.

## Benefícios de CQRS + Clean Architecture:

**1. Separação Limpa:**

- Commands focam em transações e validação

- Queries otimizadas para projeções de dados

**2. Modelos Diferentes:**

- Pode usar entidades completas para commands

- DTOs otimizados para queries

**3. Escalabilidade:**

- Possibilidade de bancos separados (read db/write db)

- Cache mais fácil no lado de leitura

**4. Manutenção:**

- Alterações em um lado não afetam o outro

- Testabilidade isolada


```plaintext
GlicareApp/
│
├── Presentation/                   
│   └── GlicareApp.Api					        # API: Camada de implementação da API Web (controllers, middlewares)
├── Application/						
│   └── GlicareApp.Services/			      # SERVICES: camada responsável pela mediação entre a interface de usuário e dominio da aplicação
│	│	├── UseCases/					              # Implementações concretas de casos de uso
│	│	├── CommandsHandlers/			          # Manipuladores de comandos (Write side)
│	│	├── QueriesHandlers/			          # Manipuladores de queries (Read side)
│	│	├── EventsHandlers/				          # Manipuladores de eventos de domínio
│	│	├── Hubs/						                # Hubs SignalR para comunicação em tempo real 
│	│	├── Interfaces/					            # Contratos de repositórios e serviços externos
│	│	├── Queries/					              # Leitura de dados
│	│	└── Commands/					              # Definições de comandos (DTOs de escrita)
│   └── GlicareApp.CrossCuting/		      # CROSSCUTING: camada onde devem ser alocadas funcionalidades que possam ser utilizadas por toda aplicação (exceto regras de domínio)
│		├── Configurations/				          # Arquivos de configurações da aplicação
│		├── Utils/						              # Arquivos utilizatios que possam ser reutilizados
│		└── Extensions/					            # Arquivos de extensão
├── Domain/								
│   └── GlicareApp.Domain/				      # DOMAIN: Camada de domínio (regras de negócio)
│		├── Entities/					              # Entidades de negócio (aggregate roots)
│		├── Dtos/						                # Objetos de transferência de dados (leitura)
│		└── DomainEvents/				            # Eventos disparados pelo domínio
├── Infrastructure/						
│   ├── GlicareApp.Repositories/		    # REPOSITORIES: Camada de implementações concretas de repositórios
│   └── GlicareApp.ExternalServices/    # EXTERNAL SERVICES: Camada de clientes de serviços externos (APIs, SMTP, etc)
├── Tests/								
│   └── GlicareApp.Tests/				        # TESTS: Camada de testes da aplicação
│		├── UnitTests/					            # Testes unitários (isolados)
│		└── IntegrationTests/			          # Testes de integração entre componentes
├── Dockerfile							            # Configuração para containerização
└── README.md							              # Documentação principal do projeto
```

## Git Flow

```plaintext
main    (protegida) - Código estável (produção)
dev     (protegida) - Integração contínua para testes
│
└── feature/*      - Branches temporárias para novas funcionalidades
└── hotfix/*       - Correções críticas para produção
```


## Commits semânticos
```plaintext
feat     → Nova funcionalidade
fix      → Correção de bug
docs     → Documentação
refactor → Refatoração sem mudança de comportamento
test     → Testes
chore    → Tarefas de manutenção
```

### Exemplo
```bash
git commit -m "feat(auth): add login with Google OAuth"
git commit -m "fix(validation): correct email format validation"
git commit -m "refactor(payment): extract processor to separate class"
git commit -m "test(service): add unit tests for invoice generator"
git commit -m "chore(deps): update .net version to 10"
```


# Glicare-BackEnd
