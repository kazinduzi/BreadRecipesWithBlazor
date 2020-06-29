using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorAppWASM.Shared.BusinessDomain
{
	public class BaseEntity
	{
		[Key]
		public int ObjectId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}
