using System.ComponentModel.DataAnnotations.Schema;

namespace App.ApplicationLayer.Domain
{ 
    [Table("Table_3", Schema = "sch1")]
    public class Table3
    {
        [Column("Id")]
        //[Key]
        public int Id { get; set; }

        [Column("Desc")]
        public string Desc { get; set; }
    }
}
