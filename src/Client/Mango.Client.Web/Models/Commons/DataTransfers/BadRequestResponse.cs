using System.Collections.Generic;

namespace Mango.Client.Web.Models.Commons.DataTransfers;

public record BadRequestResponse(
    string Type,
    string Title,
    int Status,
    IDictionary<string, string[]> Errors,
    string TraceId
);
