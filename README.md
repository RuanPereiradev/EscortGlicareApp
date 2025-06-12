
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

Utilizaremos CQRS com clean Architecture

```plaintext
GlicareAppApi/
│
├── Presentation/                   # Camada de apresentação/interface HTTP
│   └── WebApi                      # Implementação da API Web (controllers, middlewares)
├── Application/                    # Camada de aplicação (casos de uso, CQRS)
│   ├── UseCases/                   # Implementações concretas de casos de uso
│   ├── CommandsHandlers/           # Manipuladores de comandos (Write side)
│   ├── EventsHandlers/             # Manipuladores de eventos de domínio
│   ├── Hubs/                       # Hubs SignalR para comunicação em tempo real 
│   ├── Interfaces/                 # Contratos de repositórios e serviços externos
│   └── Commands/                   # Definições de comandos (DTOs de escrita)
├── Domain/                         # Camada de domínio (regras de negócio)
│   ├── Entities/                   # Entidades de negócio (aggregate roots)
│   ├── Dtos/                       # Objetos de transferência de dados (leitura)
│   └── DomainEvents/               # Eventos disparados pelo domínio
├── Infrastructure/                 # Camada de infraestrutura (implementações)
│   ├── Repositories/               # Implementações concretas de repositórios
│   └── ExternalServices/           # Clientes de serviços externos (APIs, SMTP, etc)
├── Tests/                          # Suíte de testes automatizados
│   ├── UnitTests/                  # Testes unitários (isolados)
│   └── IntegrationTests/           # Testes de integração entre componentes
├── Dockerfile                      # Configuração para containerização
└── README.md                       # Documentação principal do projeto
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


