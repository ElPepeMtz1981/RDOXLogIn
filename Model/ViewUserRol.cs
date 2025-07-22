using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDOXMES.Login;

[Table("ViewUserRol")]
public class ViewUserRol
{
    [Key]
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Name { get; set; }

    public int IdRol { get; set; }

    public string Rol { get; set; }

    public string Password { get; set; }
}