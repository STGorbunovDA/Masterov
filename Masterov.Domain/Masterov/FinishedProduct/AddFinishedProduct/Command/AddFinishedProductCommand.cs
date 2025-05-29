﻿namespace Masterov.Domain.Masterov.FinishedProduct.AddFinishedProduct.Command;

public record AddFinishedProductCommand(string Name, string Type, decimal? Price, int? Width, int? Height, int? Depth, byte[]? Content);