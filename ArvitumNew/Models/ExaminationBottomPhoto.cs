namespace ArvitumNew.Models
{
    public partial class ExaminationBottomPhoto
    {
        public int Id { get; set; }
        public int ExaminationId { get; set; }
        public string FileNameLeft { get; set; }
        public string TitleLeft { get; set; }
        public byte[] ImageDataLeft { get; set; }
        public int ShoesSizeLId { get; set; }
        public string FileNameRight { get; set; }
        public string TitleRight { get; set; }
        public byte[] ImageDataRight { get; set; }
        public int ShoesSizeRId { get; set; }
        public short LLX { get; set; }
        public short LLY { get; set; }
        public short LRX { get; set; }
        public short LRY { get; set; }
        public short LTX { get; set; }
        public short LTY { get; set; }
        public short LDX { get; set; }
        public short LDY { get; set; }
        public short RLX { get; set; }
        public short RLY { get; set; }
        public short RRX { get; set; }
        public short RRY { get; set; }
        public short RTX { get; set; }
        public short RTY { get; set; }
        public short RDX { get; set; }
        public short RDY { get; set; }
    
        public virtual Examination Examination { get; set; }
        public virtual ShoesSize ShoesSize { get; set; }
    }
}
