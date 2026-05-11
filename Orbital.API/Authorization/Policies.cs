namespace Orbital.API.Authorization
{
    public static class Policies
    {
        // PLANETAS
        public const string PlanetasRead = "Planetas.Read";
        public const string PlanetasWrite = "Planetas.Write";

        // PLANETA ESTADO
        public const string PlanetaEstadoManage = "PlanetaEstado.Manage";

        // CATÁLOGOS
        public const string CatalogosRead = "Catalogos.Read";

        // USUARIOS
        public const string UsuariosRead = "Usuarios.Read";
        public const string UsuariosWrite = "Usuarios.Write";

        // VALORACIONES
        public const string ValoracionesRead = "Valoraciones.Read";
        public const string ValoracionesWrite = "Valoraciones.Write";
        public const string ValoracionesApprove = "Valoraciones.Approve";

        // JERARQUÍAS
        public const string JerarquiasRead = "Jerarquias.Read";

        // ROLES
        public const string RolesRead = "Roles.Read";
    }
}