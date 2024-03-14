namespace trainingQuerySide.EventHandlers.MemberOut
{
    public record MemberOut(
      string AggregateId,
      int Sequence,
      MemberOutData Data,
      DateTime DateTime,
      string UserId,
      int Version
  ) : Event<MemberOutData>(
      AggregateId: AggregateId,
      Sequence: Sequence,
      Data: Data,
      DateTime: DateTime,
      UserId: UserId,
      Version: Version
  );
}
