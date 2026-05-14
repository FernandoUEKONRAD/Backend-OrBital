using Microsoft.AspNetCore.Authorization;

namespace Orbital.API.Authorization
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddCustomAuthorization(
            this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                // =========================
                //  PLANETAS
                // =========================
                options.AddPolicy(Policies.PlanetasRead, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Analista,
                        RolesIds.Especialista,
                        RolesIds.GuerreroConquista,
                        RolesIds.SistemaScouter));

                options.AddPolicy(Policies.PlanetasWrite, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.SistemaScouter));

                // =========================
                // PLANETA ESTADO
                // =========================
                options.AddPolicy(Policies.PlanetaEstadoManage, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Comandante,
                        RolesIds.Especialista,
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

                options.AddPolicy(Policies.UsuariosWrite, policy =>
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

                options.AddPolicy(Policies.ValoracionesWrite, policy =>
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
                // 🎖 ROLES
                // =========================
                options.AddPolicy(Policies.RolesRead, policy =>
                    policy.RequireClaim("Id_Rol",
                        RolesIds.Emperador,
                        RolesIds.Desarrollador,
                        RolesIds.SistemaScouter));
            });

            return services;
        }
    }
}