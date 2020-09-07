using System;

namespace TDSAMedBDDomain.Models {
  public class Specialty {
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
  }
}
