using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
            };

        /* ESCOPOS
         * São adicionados aos token de acesso e são usados pelas APIs para saber se o cliente possui acesso a tal
         * funcionalidade através dos escopos que ele possui
         * 
         * Aqui vamos declarar todos os escopos que possiveis cliente EVENTUALMENTE podem ter
         */
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
                { 
                    new ApiScope(name: "api1", displayName: "MyAPI")
                };

        public static IEnumerable<Client> Clients =>
            new Client[]
                {
                    // CLIENTE PARA ACESSO MAQ. VS MAQ.
                    new Client
                    {
                        // Identificação do cliente
                        ClientId = "client",
                        // Tipo de fluxo que será usado para autenticar esse cliente (maquina X maquina)
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        // Secret que esse cliente vai ter que usar para conseguir o token de acesso
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        // Escopos que esse cliente tem acesso
                        AllowedScopes = { "api1" }
                        
                    },
                    // ACESSO COM USUARIO
                    new Client
                    {
                        ClientId = "web",
                        ClientSecrets = { new Secret("secret".Sha256()) },
                        AllowedGrantTypes = GrantTypes.Code,
                        RedirectUris = { "https://localhost:7126/signin-oidc" },
                        PostLogoutRedirectUris = { "https://localhost:7126/signout-callback-oidc" },
                        AllowedScopes = { 
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile 
                        }
                    },
                    new Client
                    {
                        ClientId = "bff",
                        ClientSecrets = { new Secret("secret".Sha256()) },

                        AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
    
                        // where to redirect to after login
                        RedirectUris = { "https://localhost:5003/signin-oidc" },

                        // where to redirect to after logout
                        PostLogoutRedirectUris = { "https://localhost:5003/signout-callback-oidc" },

                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "api1"
                        }
                    }
                };
    }
}