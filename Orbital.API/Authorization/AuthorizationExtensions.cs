using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Orbital.API.Authorization
{ //temporal
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddCustomAuthorization(
            this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // =========================
                // PLANETAS
                // =========================
                options.AddPolicy(Policies.PlanetasRead, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Analista,
                        RolesIds.Especialista,
                        RolesIds.GuerreroConquista,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.PlanetasCreate, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.PlanetasUpdate, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.PlanetasDelete, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.SistemaScouter));


                // =========================
                // PLANETA ESTADO
                // =========================
                options.AddPolicy(Policies.PlanetaEstadoRead, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Especialista,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.PlanetaEstadoCreate, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.PlanetaEstadoUpdate, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.PlanetaEstadoDelete, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.SistemaScouter));


                // =========================
                // CATÁLOGOS
                // =========================
                options.AddPolicy(Policies.CatalogosRead, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Analista,
                        RolesIds.Desarrollador,
                        RolesIds.Especialista,
                        RolesIds.Gestor,
                        RolesIds.GuerreroConquista,
                        RolesIds.SistemaScouter));


                // =========================
                // USUARIOS
                // =========================
                options.AddPolicy(Policies.UsuariosRead, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Desarrollador,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.UsuariosCreate, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Desarrollador,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.UsuariosUpdate, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Desarrollador,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.UsuariosDelete, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Desarrollador,
                        RolesIds.SistemaScouter));


                // =========================
                // VALORACIONES
                // =========================
                options.AddPolicy(Policies.ValoracionesRead, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Analista,
                        RolesIds.Especialista,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.ValoracionesCreate, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Analista,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.ValoracionesUpdate, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Analista,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.ValoracionesDelete, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Analista,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.ValoracionesApprove, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.SistemaScouter));


                // =========================
                // JERARQUÍAS
                // =========================
                options.AddPolicy(Policies.JerarquiasRead, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Analista,
                        RolesIds.SistemaScouter));


                // =========================
                // ROLES
                // =========================
                options.AddPolicy(Policies.RolesRead, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Desarrollador,
                        RolesIds.SistemaScouter));

                 // =========================
                // Valoraciones
                // =========================
            

                options.AddPolicy(Policies.ValoracionesRead, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Analista,
                        RolesIds.Especialista,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.ValoracionesCreate, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Analista,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.ValoracionesReject, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Analista,
                        RolesIds.SistemaScouter));


                options.AddPolicy(Policies.ValoracionesApprove, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.SistemaScouter));

                // =========================
                // MERCADO INTERESTELAR
                // =========================
                options.AddPolicy(Policies.MercadoPublicar, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Gestor));

                options.AddPolicy(Policies.MercadoEditar, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Gestor));

                options.AddPolicy(Policies.MercadoRetirar, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Gestor));

                // =========================
                // TRANSACCIONES
                // =========================
                options.AddPolicy(Policies.TransaccionesLeer, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Gestor,
                        RolesIds.Analista));

                options.AddPolicy(Policies.TransaccionesGestionar, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Gestor));

                // =========================
                // CLIENTES (administración interna)
                // =========================
                options.AddPolicy(Policies.ClientesAdministrar, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Gestor));

                // Política exclusiva para clientes externos (JWT con claim tipo=cliente)
                options.AddPolicy(Policies.ClienteAutenticado, policy =>
                    policy.RequireClaim("tipo", "cliente"));

                // =========================
                // REPORTES
                // =========================
                options.AddPolicy(Policies.ReportesLeer, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Analista));
            });

            return services;
        }
    }
}