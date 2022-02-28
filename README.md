# Identity Server

### Clients (Clientes)

São softwares que querem acessar algum recurso protegido pelo Identity Server

### Resources (Recursos)

Recursos que o identity server protege. Podem ser APIs ou dados dos usuários.

### Users (Usuarios)

Pessoas que tem seus dados gerenciados pelo identity server e que utilizam um cliente para acessar seus dados ou outras APIs protegidas pelo identity server

### Identity token

Token fornecido pelo IS para softwares clientes que contém informações mínimas sobre o usuário assim como informações sobre a hora da autenticação

### Access Token

Token fornecido pelo IS para softwares clientes que contém os escopos dos recursos que determinado cliente tem acesso.

## Authentication vs Authorization

Autenticação é o processo de validar a identidade de alguém.

A Autorização é o processo de checar se determinado usuário ou cliente tem *acesso* à determinada funcionalidade.