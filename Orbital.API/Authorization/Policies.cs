namespace Orbital.API.Authorization
{
    public static class Policies
    {
        // PLANETAS
        public const string PlanetasRead = "Planetas.Read";
        public const string PlanetasCreate = "Planetas.Create";
        public const string PlanetasUpdate = "Planetas.Update";
        public const string PlanetasDelete = "Planetas.Delete";
        

        // PLANETA ESTADO
        public const string PlanetaEstadoRead   = "PlanetaEstado.Read";
        public const string PlanetaEstadoCreate = "PlanetaEstado.Create";
        public const string PlanetaEstadoUpdate = "PlanetaEstado.Update";
        public const string PlanetaEstadoDelete = "PlanetaEstado.Delete";
        public const string PlanetaEstadoManage = "PlanetaEstado.Manage";

        // CATÁLOGOS
        public const string CatalogosRead = "Catalogos.Read";

        // USUARIOS
        public const string UsuariosRead = "Usuarios.Read";
        public const string UsuariosCreate = "Usuarios.Create";
        public const string UsuariosUpdate = "Usuarios.Update";
        public const string UsuariosDelete = "Usuarios.Delete";

        // VALORACIONES
        public const string ValoracionesRead = "Valoraciones.Read";
        public const string ValoracionesCreate  = "Valoraciones.Create";
        public const string ValoracionesApprove = "Valoraciones.Approve";
        public const string ValoracionesReject  = "Valoraciones.Reject";

        // JERARQUÍAS
        public const string JerarquiasRead = "Jerarquias.Read";

        // ROLES
        public const string RolesRead = "Roles.Read";
    }
}