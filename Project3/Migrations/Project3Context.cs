using System;
using System.Collections.Generic;
using Project3.Models;
using Microsoft.EntityFrameworkCore;

namespace Project3.Migrations;

public partial class Project3Context : DbContext
{
    public Project3Context()
    {
    }

    public Project3Context(DbContextOptions<Project3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<AnswerQuestion> AnswerQuestions { get; set; }

    public virtual DbSet<AnswerQuestionChose> AnswerQuestionChoses { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<CategoryBlog> CategoryBlogs { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<HistoryExam> HistoryExams { get; set; }

    public virtual DbSet<InformationStudent> InformationStudents { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<AnswerQuestion> AnswerQuestions { get; set; }

    public virtual DbSet<AnswerQuestionChose> AnswerQuestionChoses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-ASJD1ADE;Initial Catalog=Project3;TrustServerCertificate=True;Persist Security Info=True;User ID=sa;Password=123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnswerQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0736855471");

            entity.ToTable("AnswerQuestion");

            entity.Property(e => e.AnswerQuestion1)
                .HasMaxLength(2000)
                .HasColumnName("AnswerQuestion");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");

            entity.HasOne(d => d.Question).WithMany(p => p.AnswerQuestions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__AnswerQue__Quest__72C60C4A");
        });

        modelBuilder.Entity<AnswerQuestionChose>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07A686C864");

            entity.ToTable("AnswerQuestionChose");

            entity.Property(e => e.AnswerChose).HasMaxLength(2000);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.HistoryExam).WithMany(p => p.AnswerQuestionChoses)
                .HasForeignKey(d => d.HistoryExamId)
                .HasConstraintName("FK__AnswerQue__Histo__6FE99F9F");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0764C28E1E");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Summay).HasColumnType("text");
            entity.Property(e => e.Title).HasMaxLength(2000);
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Blogs__CategoryI__66603565");

            entity.HasOne(d => d.Users).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__Blogs__UsersId__00200768");
        });

        modelBuilder.Entity<CategoryBlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0723228CA6");

            entity.Property(e => e.CategoryName).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07DF34C5F0");

            entity.Property(e => e.CoursesName).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Level)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("level");
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07A904A0C2");

            entity.ToTable("Exam");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.NameExam).HasMaxLength(150);
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<HistoryExam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC076C5BB604");

            entity.ToTable("HistoryExam");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.Exam).WithMany(p => p.HistoryExams)
                .HasForeignKey(d => d.ExamId)
                .HasConstraintName("FK__HistoryEx__ExamI__6EF57B66");
        });

        modelBuilder.Entity<InformationStudent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Informat__3214EC07A9EBF0A5");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DateBirth).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EndCard).HasColumnType("datetime");
            entity.Property(e => e.FromCard).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IdCardStudent).HasMaxLength(255);
            entity.Property(e => e.IdentityCard).HasMaxLength(255);
            entity.Property(e => e.StartCard).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            entity.Property(e => e.WasBorn).HasMaxLength(200);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC079E10C081");

            entity.ToTable("Question");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.NameQuestion).HasMaxLength(2000);
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");

            entity.HasOne(d => d.Exam).WithMany(p => p.Questions)
                .HasForeignKey(d => d.ExamId)
                .HasConstraintName("FK__Question__ExamId__73BA3083");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07C65539F8");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.NameRole).HasMaxLength(255);
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07AF73DF47");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("price");
            entity.Property(e => e.SubjectName).HasMaxLength(200);
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");

            entity.HasOne(d => d.Courses).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.CoursesId)
                .HasConstraintName("FK__Subjects__Course__797309D9");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0788589BFD");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07293B4BA9");

            entity.ToTable("UserRole");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserRole__RoleId__7D439ABD");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRole__UserId__02084FDA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
