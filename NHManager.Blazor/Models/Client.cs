
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NHManager.Blazor.Models;

public class Client : BaseModelObject
{
	[Required]
	[MaxLength(100)]
	
	public string FirstName { get; set; } = null!;

	[Required]
	[MaxLength(100)]
	
	public string SurName { get; set; } = null!;

	[Required]
	[DataType(DataType.Date)]
	
	public DateTime DateOfBirth { get; set; }

	[MaxLength(100)]
	
	public string? Street { get; set; }

	[MaxLength(100)]
	
	public string? City { get; set; }

	[MaxLength(100)]
	
	public string? PostalCode { get; set; }

	[MaxLength(100)]
	
	public string? Country { get; set; }

	[MaxLength(100)]
	
	public string? Phone { get; set; }

	[MaxLength(100)]
	
	public string? Email { get; set; }

	
	public string? Notes { get; set; }

	[NotMapped]
	
	public string FullName => $"{FirstName} {SurName}";

	[NotMapped]
	
	public string FullNameWithPhone => $"{FirstName} {SurName} {Phone}";

	public int? ConsultantId { get; set; }

	[ForeignKey("ConsultantId")]
	[DeleteBehavior(DeleteBehavior.NoAction)]
	
	public Worker? Consultant { get; set; }

	[NotMapped]
	
	public string ConsultantFullName => $"{Consultant?.FirstName} {Consultant?.SurName}";


	[DataType(DataType.Date)]
	
	public DateTime? CarePayTo { get; set; }

	
	public int Sex { get; set; } = 1;

	[InverseProperty("Client")]
	public ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();

	[InverseProperty("Client")]
	public ICollection<ClientDocument> Documents { get; set; } = new List<ClientDocument>();

	[InverseProperty("Client")]
	public ICollection<ClientMeasurement> Measurements { get; set; } = new List<ClientMeasurement>();

	[InverseProperty("Client")]
	public ICollection<ClientEvent> Events { get; set; } = new List<ClientEvent>();

	[InverseProperty("Client")]
	public ICollection<ClientAnalysis> Analysis { get; set; } = new List<ClientAnalysis>();

	[InverseProperty("Client")]
	public ICollection<ClientQuestionnaire> Questionnaires { get; set; } = new List<ClientQuestionnaire>();

	[InverseProperty("Client")]
	public ICollection<ClientBiochemistry> Biochemistry { get; set; } = new List<ClientBiochemistry>();

	[InverseProperty("Client")]
	public ICollection<ClientAnalysisResult> AnalysisResults { get; set; } = new List<ClientAnalysisResult>();

	[InverseProperty("Client")]
	public ICollection<ClientMeasurementResult> MeasurementResults { get; set; } = new List<ClientMeasurementResult>();

	[InverseProperty("Client")]
	public ICollection<ClientQuestionnaireResult> QuestionnaireResults { get; set; } = new List<ClientQuestionnaireResult>();

	[InverseProperty("Client")]
	public ICollection<ClientCookBook> CookBooks { get; set; } = new List<ClientCookBook>();

	[InverseProperty("Client")]
	public ICollection<Order> Orders { get; set; } = new List<Order>();

}
