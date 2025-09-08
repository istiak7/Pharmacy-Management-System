namespace Pharmacy_Management_System.DependencyExtensions
{
    public static class JWTAuthentication
    {
        public static void AddJWTAuthentication(this WebApplicationBuilder builder)
        {
            var jwtSettings = new JWTSettings();
        }
    }
}
