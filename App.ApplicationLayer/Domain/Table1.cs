using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.ApplicationLayer.Domain
{
    [Table("Table_1", Schema = "dbo")]
    public class Table1
    {
        [Column("Id")]
        //[Key]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

    }
}
