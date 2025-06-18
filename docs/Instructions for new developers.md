# Guia para Novos Desenvolvedores - GlicareApp Backend

## 🎯 Sobre o Projeto

O **GlicareApp** é um aplicativo dedicado a pacientes de diabetes, médicos, nutricionistas e demais profissionais da saúde que desejam maior simplicidade, praticidade e facilidade no acompanhamento, controle da evolução da doença e sintomas.

### Funcionalidades Principais
- ✅ Autenticação
- ✅ Controle de glicemia
- ✅ Gerenciamento de usuário
- ✅ Feedbacks
- ✅ Configurações
- ✅ Notificações

## 🛠️ Stack Tecnológica

| Componente | Tecnologia |
|------------|------------|
| **Backend** | C# (ASP.NET 8) |
| **Banco de Dados** | PostgreSQL, MongoDB |
| **Libraries** | MediaTR, FluentValidation, Dapper, SignalR |
| **Infraestrutura** | GCP - Cloud Run (Docker) |

## 🏗️ Arquitetura do Projeto

### Por que CQRS + Clean Architecture?

O projeto utiliza **CQRS (Command Query Responsibility Segregation)** combinado com **Clean Architecture** para:

- **Separação Limpa**: Commands focam em transações e validação, Queries otimizadas para projeções
- **Modelos Diferentes**: Entidades completas para commands, DTOs otimizados para queries
- **Escalabilidade**: Possibilidade de bancos separados (read/write)
- **Manutenção**: Alterações isoladas e testabilidade independente

### Estrutura de Pastas

```
GlicareApp/
│
├── Presentation/                   
│   └── GlicareApp.Api/            # API: Controllers, middlewares
├── Application/						
│   └── GlicareApp.Services/       # SERVICES: Mediação entre UI e domínio
│	│	├── UseCases/              # Implementações de casos de uso
│	│	├── CommandsHandlers/      # Manipuladores de comandos (Write)
│	│	├── QueriesHandlers/       # Manipuladores de queries (Read)
│	│	├── EventsHandlers/        # Manipuladores de eventos
│	│	├── Hubs/                  # SignalR para tempo real
│	│	├── Interfaces/            # Contratos de repositórios
│	│	├── Queries/               # Leitura de dados
│	│	└── Commands/              # Definições de comandos (DTOs)
│   └── GlicareApp.CrossCuting/    # CROSSCUTING: Funcionalidades compartilhadas
│		├── Configurations/        # Configurações da aplicação
│		├── Utils/                 # Utilitários reutilizáveis
│		└── Extensions/            # Extensões
├── Domain/								
│   └── GlicareApp.Domain/         # DOMAIN: Regras de negócio
│		├── Entities/              # Entidades de negócio
│		├── Dtos/                  # DTOs de leitura
│		└── DomainEvents/          # Eventos de domínio
├── Infrastructure/						
│   ├── GlicareApp.Repositories/   # REPOSITORIES: Implementações de repositórios
│   └── GlicareApp.ExternalServices/ # EXTERNAL SERVICES: Clientes externos
├── Tests/								
│   └── GlicareApp.Tests/          # TESTS: Testes da aplicação
│		├── UnitTests/             # Testes unitários
│		└── IntegrationTests/      # Testes de integração
├── Dockerfile                     # Configuração Docker
└── README.md                      # Documentação principal
```

## 🚀 Setup do Ambiente de Desenvolvimento

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Git](https://git-scm.com/)

### Configuração Inicial

1. **Clone o repositório**
   ```bash
   git clone [URL_DO_REPOSITORIO]
   cd GlicareAppBackend
   ```

2. **Restaurar dependências**
   ```bash
   dotnet restore
   ```

3. **Configurar bancos de dados**
   - PostgreSQL: Configure a string de conexão em `appsettings.json`
   - MongoDB: Configure a string de conexão em `appsettings.json`

4. **Executar migrações** (se aplicável)
   ```bash
   dotnet ef database update
   ```

5. **Executar o projeto**
   ```bash
   dotnet run --project GlicareApp.Api
   ```

## 📋 Workflow de Desenvolvimento

### Git Flow

```plaintext
main    (protegida) - Código estável (produção)
dev     (protegida) - Integração contínua para testes
│
└── feature/*      - Branches temporárias para novas funcionalidades
└── hotfix/*       - Correções críticas para produção
```

### Processo de Desenvolvimento

1. **Criar branch de feature**
   ```bash
   git checkout dev
   git pull origin dev
   git checkout -b feature/nome-da-funcionalidade
   ```

2. **Desenvolver e commitar**
   - Faça commits frequentes e pequenos
   - Use commits semânticos (veja seção abaixo)

3. **Push e Pull Request**
   ```bash
   git push origin feature/nome-da-funcionalidade
   # Criar Pull Request para dev
   - Enviar link do PR no grupo de back-end do discord
   ```

4. **Code Review**
   - Aguarde aprovação de qualquer outro membro do time
   - Resolva conflitos se necessário

5. **Merge**
   - Após aprovação, merge para dev

## 📝 Padrão de Commits Semânticos

### Tipos de Commit
- `feat` → Nova funcionalidade
- `fix` → Correção de bug
- `docs` → Documentação
- `refactor` → Refatoração sem mudança de comportamento
- `test` → Testes
- `chore` → Tarefas de manutenção

### Exemplos
```bash
git commit -m "feat(auth): add login with Google OAuth"
git commit -m "fix(validation): correct email format validation"
git commit -m "refactor(payment): extract processor to separate class"
git commit -m "test(service): add unit tests for invoice generator"
git commit -m "chore(deps): update .net version to 10"
```

## 🧪 Testes

### Executar Testes
```bash
# Todos os testes
dotnet test

# Testes específicos
dotnet test --filter "Category=Unit"
dotnet test --filter "Category=Integration"
```

### Cobertura de Testes
- Mantenha cobertura mínima de 80%
- Testes unitários para lógica de negócio
- Testes de integração para APIs e repositórios

## 🐳 Docker

### Build da Imagem
```bash
docker build -t glicareapp-backend .
```

### Executar Container
```bash
docker run -p 5000:5000 glicareapp-backend
```

## 📚 Recursos Importantes

### Documentação
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [CQRS Pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [MediatR](https://github.com/jbogard/MediatR)
- [FluentValidation](https://fluentvalidation.net/)

### Boas Práticas
- ✅ Sempre use CQRS para separar leitura e escrita
- ✅ Mantenha as regras de negócio no Domain
- ✅ Use DTOs para transferência de dados
- ✅ Implemente validações com FluentValidation
- ✅ Escreva testes para novas funcionalidades
- ✅ Documente APIs com Swagger
- ✅ Use logging estruturado

### Evite
- ❌ Lógica de negócio em Controllers
- ❌ Dependências circulares entre camadas
- ❌ Commits grandes e sem foco
- ❌ Hardcoding de configurações
- ❌ Ignorar tratamento de erros

## 🆘 Suporte e Comunicação

### Canais de Comunicação
- **Discord**: [Canal de texto e voz de back-end]
- **Email**: [EMAIL_DO_TIME]
- **GitHub**: [https://github.com/orgs/Projeto-Glicare/projects/3]

### Mentores
- **Tech Lead**: [Breno Machado]
- **Tech Lead**: [David ] - [CONTATO]

### Reuniões
- **Daily**: [A definir] - [Discord]
- **Sprint Planning**: [A definir] - [Discord]
- **Code Review**: [Compartilhar link do grupo do discord] - [Discord]

## 🎯 Próximos Passos

1. **Configurar ambiente** seguindo este guia
2. **Fazer primeiro commit** seguindo padrões semânticos
3. **Criar Pull Request** para uma issue simples
4. **Participar do code review** de outros desenvolvedores
5. **Estudar a arquitetura** CQRS e Clean Architecture
6. **Familiarizar-se** com as bibliotecas utilizadas

---

**Bem-vindo ao time GlicareApp! 🚀**

Se tiver dúvidas, não hesite em perguntar. Estamos aqui para ajudar!
