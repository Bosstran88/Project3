﻿namespace Project3.Entity.Request
{
    public class AddAnswerQuestionReq
    {
        public long? Id { get; set; }
        public long? QuestionId { get; set; }
        public string? AnswerQuestion1 { get; set; }
        public int? Score { get; set; }
    }
}
