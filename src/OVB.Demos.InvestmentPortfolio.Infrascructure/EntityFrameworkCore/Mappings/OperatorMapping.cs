using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OVB.Demos.InvestmentPortfolio.Domain.BoundedContexts.OperatorContext.DataTransferObject;
using OVB.Demos.InvestmentPortfolio.Domain.ValueObjects;

namespace OVB.Demos.InvestmentPortfolio.Infrascructure.EntityFrameworkCore.Mappings;

public sealed class OperatorMapping : IEntityTypeConfiguration<Operator>
{
    public void Configure(EntityTypeBuilder<Operator> builder)
    {
        #region Table Configuration

        builder.ToTable(
            name: "operators",
            schema: "operator");

        #endregion

        #region Primary Key Configuration

        builder.HasKey(p => p.Id)
            .HasName("pk_operator_id");

        #endregion

        #region Foreign Key Configuration



        #endregion

        #region Index Key Configuration

        builder.HasIndex(p => p.Code)
            .IsUnique(true)
            .HasDatabaseName("uk_operator_code");

        builder.HasIndex(p => p.Email)
            .IsUnique(true)
            .HasDatabaseName("uk_operator_email");

        #endregion

        #region Properties Configuration

        builder.Property(p => p.Id)
            .IsRequired(true)
            .IsFixedLength(true)
            .HasColumnType("CHAR")
            .HasColumnName("idoperator")
            .HasMaxLength(Guid.Empty.ToString().Length)
            .HasConversion(p => p.GetIdentityAsString(), p => IdentityValueObject.Factory(Guid.Parse(p)))
            .ValueGeneratedNever();

        builder.Property(p => p.Code)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("VARCHAR")
            .HasColumnName("code")
            .HasMaxLength(CodeValueObject.MAX_LENGTH)
            .HasConversion(p => p.GetCode(), p => CodeValueObject.Factory(p))
            .ValueGeneratedNever();
        builder.Property(p => p.Name)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("VARCHAR")
            .HasColumnName("name")
            .HasMaxLength(NameValueObject.MAX_LENGTH)
            .HasConversion(p => p.GetName(), p => NameValueObject.Factory(p))
            .ValueGeneratedNever();
        builder.Property(p => p.Document)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("VARCHAR")
            .HasColumnName("document")
            .HasMaxLength(DocumentValueObject.CNPJ_DOCUMENT_REQUIRED_LENGTH)
            .HasConversion(p => p.GetDocument(), p => DocumentValueObject.Factory(p))
            .ValueGeneratedNever();
        builder.Property(p => p.Email)
            .IsRequired(true)
            .IsFixedLength(false)
            .HasColumnType("VARCHAR")
            .HasColumnName("email")
            .HasMaxLength(EmailValueObject.MAX_LENGTH)
            .HasConversion(p => p.GetEmail(), p => EmailValueObject.Factory(p))
            .ValueGeneratedNever();
        builder.Property(p => p.PasswordHash)
            .IsFixedLength(true)
            .IsRequired(true)
            .HasColumnName("password_hash")
            .HasColumnType("CHAR")
            .HasMaxLength(64)
            .ValueGeneratedNever();
        builder.Property(p => p.Salt)
            .IsFixedLength(false)
            .IsRequired(true)
            .HasColumnName("password_salt")
            .HasColumnType("VARCHAR")
            .HasMaxLength(64)
            .ValueGeneratedNever();

        #endregion
    }
}
