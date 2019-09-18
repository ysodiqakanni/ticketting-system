using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TickettingSystem.Data.DbModel
{
    public partial class db_a3d3ad_pricingContext : DbContext
    {
        public db_a3d3ad_pricingContext()
        {
        }

        public db_a3d3ad_pricingContext(DbContextOptions<db_a3d3ad_pricingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clientinterest> Clientinterest { get; set; }
        public virtual DbSet<CurrentPrice> CurrentPrice { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<ExchangeType> ExchangeType { get; set; }
        public virtual DbSet<Exchangesmaster> Exchangesmaster { get; set; }
        public virtual DbSet<Exchangesusers> Exchangesusers { get; set; }
        public virtual DbSet<Languages> Languages { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<NotificationsUserDetails> NotificationsUserDetails { get; set; }
        public virtual DbSet<OldPrice> OldPrice { get; set; }
        public virtual DbSet<Socialtradingproviders> Socialtradingproviders { get; set; }
        public virtual DbSet<StaffDetails> StaffDetails { get; set; }
        public virtual DbSet<StaffLanguages> StaffLanguages { get; set; }
        public virtual DbSet<StaffNotes> StaffNotes { get; set; }
        public virtual DbSet<StaffTerritory> StaffTerritory { get; set; }
        public virtual DbSet<SupportDepartementType> SupportDepartementType { get; set; }
        public virtual DbSet<SupportTicket> SupportTicket { get; set; }
        public virtual DbSet<Territories> Territories { get; set; }
        public virtual DbSet<TradeFollow> TradeFollow { get; set; }
        public virtual DbSet<TradeLog> TradeLog { get; set; }
        public virtual DbSet<TradeType> TradeType { get; set; }
        public virtual DbSet<UserActionType> UserActionType { get; set; }
        public virtual DbSet<UserActivityLog> UserActivityLog { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }
        public virtual DbSet<UserDocs> UserDocs { get; set; }
        public virtual DbSet<UserNotes> UserNotes { get; set; }
        public virtual DbSet<UserPwd> UserPwd { get; set; }
        public virtual DbSet<UserVerification> UserVerification { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=root;database=ticketing_db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Clientinterest>(entity =>
            {
                entity.ToTable("clientinterest", "ticketing_db");

                entity.HasIndex(e => e.Emailaddress)
                    .HasName("emailaddress_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Phonenumber)
                    .HasName("phonenumber_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Emailaddress)
                    .HasColumnName("emailaddress")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Housenumber)
                    .HasColumnName("housenumber")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Phonenumber)
                    .HasColumnName("phonenumber")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Streetname1)
                    .HasColumnName("streetname1")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Streetname2)
                    .HasColumnName("streetname2")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Streetname3)
                    .HasColumnName("streetname3")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CurrentPrice>(entity =>
            {
                entity.HasKey(e => e.CpId);

                entity.ToTable("current_price", "ticketing_db");

                entity.HasIndex(e => e.CSymbol)
                    .HasName("IDX_Symbol");

                entity.HasIndex(e => e.CpId)
                    .HasName("ID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CpId)
                    .HasColumnName("cp_ID")
                    .HasColumnType("int(20)");

                entity.Property(e => e.CAskPrice)
                    .HasColumnName("c_Ask_price")
                    .HasColumnType("float(24,18)");

                entity.Property(e => e.CBidPrice)
                    .HasColumnName("c_Bid_price")
                    .HasColumnType("float(24,18)");

                entity.Property(e => e.CDt).HasColumnName("c_DT");

                entity.Property(e => e.CSymbol)
                    .IsRequired()
                    .HasColumnName("c_Symbol")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CpSource)
                    .IsRequired()
                    .HasColumnName("cp_Source")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Departments>(entity =>
            {
                entity.HasKey(e => e.DeptName);

                entity.ToTable("departments", "ticketing_db");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.DeptName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DeptMgr)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DtCreated)
                    .HasColumnName("dt_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DtModified).HasColumnName("dt_modified");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<ExchangeType>(entity =>
            {
                entity.ToTable("exchange_type", "ticketing_db");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Exchangesmaster>(entity =>
            {
                entity.HasKey(e => e.ExchangeId);

                entity.ToTable("exchangesmaster", "ticketing_db");

                entity.HasIndex(e => e.ExchangeId)
                    .HasName("exchangeID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ExchangeId)
                    .HasColumnName("exchangeID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.ExchangeCanDt).HasColumnName("exchange_can_dt");

                entity.Property(e => e.ExchangeCrDt)
                    .HasColumnName("exchange_cr_dt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ExchangeModDt).HasColumnName("exchange_mod_dt");

                entity.Property(e => e.ExchangeName)
                    .IsRequired()
                    .HasColumnName("exchangeName")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeSignupUrl)
                    .IsRequired()
                    .HasColumnName("exchangeSignupURL")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeValid)
                    .HasColumnName("exchangeValid")
                    .HasColumnType("tinyint(4)");
            });

            modelBuilder.Entity<Exchangesusers>(entity =>
            {
                entity.HasKey(e => e.EuId);

                entity.ToTable("exchangesusers", "ticketing_db");

                entity.Property(e => e.EuId)
                    .HasColumnName("euID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ExchangeuCanDt).HasColumnName("exchangeu_can_dt");

                entity.Property(e => e.ExchangeuCrDt)
                    .HasColumnName("exchangeu_cr_dt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ExchangeuId)
                    .HasColumnName("exchangeuID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ExchangeuModDt).HasColumnName("exchangeu_mod_dt");

                entity.Property(e => e.ExchangeuStatus)
                    .HasColumnName("exchangeu_status")
                    .HasColumnType("int(2)");

                entity.Property(e => e.ExchangeuUserid1)
                    .HasColumnName("exchangeu_userid")
                    .HasMaxLength(4096)
                    .IsUnicode(false);

                entity.Property(e => e.Exchangeuapi1)
                    .IsRequired()
                    .HasColumnName("exchangeuapi1")
                    .HasMaxLength(4096)
                    .IsUnicode(false);

                entity.Property(e => e.Exchangeuapi2)
                    .IsRequired()
                    .HasColumnName("exchangeuapi2")
                    .HasMaxLength(4096)
                    .IsUnicode(false);

                entity.Property(e => e.Exchangeunonce)
                    .HasColumnName("exchangeunonce")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Exchangeuuserid)
                    .HasColumnName("exchangeuuserid")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Languages>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("languages", "ticketing_db");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Language)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DtCreated)
                    .HasColumnName("dt_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DtModified).HasColumnName("dt_modified");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasKey(e => e.Notificationid);

                entity.ToTable("notifications", "ticketing_db");

                entity.HasIndex(e => e.Notificationid)
                    .HasName("notificationID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Notificationid)
                    .HasColumnName("notificationid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NCondition)
                    .HasColumnName("n_condition")
                    .HasColumnType("int(2)");

                entity.Property(e => e.NConditionValue)
                    .HasColumnName("n_condition_value")
                    .HasColumnType("double(12,5)");

                entity.Property(e => e.NCurrPair)
                    .HasColumnName("n_currPair")
                    .HasColumnType("int(4)");

                entity.Property(e => e.NDtCancelled).HasColumnName("n_dt_cancelled");

                entity.Property(e => e.NDtExecuted).HasColumnName("n_dt_executed");

                entity.Property(e => e.NDtRequested)
                    .HasColumnName("n_dt_requested")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.NStatus)
                    .HasColumnName("n_status")
                    .HasColumnType("int(2)");

                entity.Property(e => e.NUserid)
                    .HasColumnName("n_userid")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<NotificationsUserDetails>(entity =>
            {
                entity.HasKey(e => e.NudUserid);

                entity.ToTable("notifications_user_details", "ticketing_db");

                entity.HasIndex(e => e.NudUserid)
                    .HasName("nud_userid_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.NudUserid)
                    .HasColumnName("nud_userid")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.NudDtCreated)
                    .IsRequired()
                    .HasColumnName("nud_dt_created")
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.NudDtModified)
                    .HasColumnName("nud_dt_modified")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.NudEmail)
                    .HasColumnName("nud_email")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.NudNotifymethod)
                    .HasColumnName("nud_notifymethod")
                    .HasColumnType("int(2)");

                entity.Property(e => e.NudSmsnumber)
                    .HasColumnName("nud_smsnumber")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.NudTelegramnumber)
                    .HasColumnName("nud_telegramnumber")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.NudUser)
                    .WithOne(p => p.NotificationsUserDetails)
                    .HasPrincipalKey<UserDetails>(p => p.Id)
                    .HasForeignKey<NotificationsUserDetails>(d => d.NudUserid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_notifications_user_details");
            });

            modelBuilder.Entity<OldPrice>(entity =>
            {
                entity.HasKey(e => e.PId);

                entity.ToTable("old_price", "ticketing_db");

                entity.HasIndex(e => e.PId)
                    .HasName("ID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.PId)
                    .HasColumnName("p_ID")
                    .HasColumnType("int(20)");

                entity.Property(e => e.AskPrice)
                    .HasColumnName("Ask_price")
                    .HasColumnType("float(24,18)");

                entity.Property(e => e.BidPrice)
                    .HasColumnName("Bid_price")
                    .HasColumnType("float(24,18)");

                entity.Property(e => e.Dt).HasColumnName("DT");

                entity.Property(e => e.PSource)
                    .IsRequired()
                    .HasColumnName("p_Source")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Socialtradingproviders>(entity =>
            {
                entity.HasKey(e => e.TraderUid);

                entity.ToTable("socialtradingproviders", "ticketing_db");

                entity.Property(e => e.TraderUid)
                    .HasColumnName("TraderUID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Strategyactive)
                    .IsRequired()
                    .HasColumnName("strategyactive")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Strategycancelledon).HasColumnName("strategycancelledon");

                entity.Property(e => e.Strategydescription)
                    .IsRequired()
                    .HasColumnName("strategydescription")
                    .HasMaxLength(245)
                    .IsUnicode(false);

                entity.Property(e => e.Strategymodifiedon).HasColumnName("strategymodifiedon");

                entity.Property(e => e.Strategystartedon).HasColumnName("strategystartedon");

                entity.Property(e => e.Strategytitle)
                    .IsRequired()
                    .HasColumnName("strategytitle")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StaffDetails>(entity =>
            {
                entity.HasKey(e => e.Staffuserid);

                entity.ToTable("staff_details", "ticketing_db");

                entity.HasIndex(e => e.Emailaddress)
                    .HasName("emailaddress_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Phonenumber)
                    .HasName("phonenumber_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Staffuserid)
                    .HasName("userid_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Staffuserid)
                    .HasColumnName("staffuserid")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Countrycode)
                    .HasColumnName("countrycode")
                    .HasColumnType("int(5)");

                entity.Property(e => e.Departmentid).HasColumnType("int(11)");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.DtCreated)
                    .HasColumnName("dt_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DtModified).HasColumnName("dt_modified");

                entity.Property(e => e.Emailaddress)
                    .HasColumnName("emailaddress")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Firedon).HasColumnName("firedon");

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.HiredOn).HasColumnName("hiredOn");

                entity.Property(e => e.Hiredbyid)
                    .IsRequired()
                    .HasColumnName("hiredbyid")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Housenumber)
                    .HasColumnName("housenumber")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Phonenumber)
                    .HasColumnName("phonenumber")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Resignedon).HasColumnName("resignedon");

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasColumnType("int(11)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Streetname1)
                    .HasColumnName("streetname1")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Streetname2)
                    .HasColumnName("streetname2")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Streetname3)
                    .HasColumnName("streetname3")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StaffLanguages>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("staff_languages", "ticketing_db");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Staffuserid)
                    .HasColumnName("staffuserid")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DtCreated)
                    .HasColumnName("dt_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DtModified).HasColumnName("dt_modified");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Languageid)
                    .HasColumnName("languageid")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<StaffNotes>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("staff_notes", "ticketing_db");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Createdby)
                    .HasColumnName("createdby")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DtCreated)
                    .HasColumnName("dt_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DtModified).HasColumnName("dt_modified");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Modifiedby)
                    .HasColumnName("modifiedby")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StaffTerritory>(entity =>
            {
                entity.HasKey(e => e.Staffuserid);

                entity.ToTable("staff_territory", "ticketing_db");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Staffuserid)
                    .HasColumnName("staffuserid")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DtCreated)
                    .HasColumnName("dt_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DtModified).HasColumnName("dt_modified");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Territory)
                    .HasColumnName("territory")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<SupportDepartementType>(entity =>
            {
                entity.ToTable("support_departement_type", "ticketing_db");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SupportTicket>(entity =>
            {
                entity.ToTable("support_ticket", "ticketing_db");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AssignedTo)
                    .HasColumnName("assigned_to")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentTypeId)
                    .HasColumnName("departmentTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DtClosed).HasColumnName("dt_closed");

                entity.Property(e => e.DtCreated).HasColumnName("dt_created");

                entity.Property(e => e.DtUpdated).HasColumnName("dt_updated");

                entity.Property(e => e.Message)
                    .HasColumnName("message")
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.ParentTicketId)
                    .HasColumnName("parent_ticket_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Subject)
                    .HasColumnName("subject")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Territories>(entity =>
            {
                entity.HasKey(e => e.TerritoryName);

                entity.ToTable("territories", "ticketing_db");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.TerritoryName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DtCreated)
                    .HasColumnName("dt_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DtModified).HasColumnName("dt_modified");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TerritoryContinent)
                    .HasColumnName("territoryContinent")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TradeFollow>(entity =>
            {
                entity.HasKey(e => e.TraderUid);

                entity.ToTable("trade_follow", "ticketing_db");

                entity.Property(e => e.TraderUid)
                    .HasColumnName("traderUID")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(22,18)");

                entity.Property(e => e.CurrencyPair)
                    .IsRequired()
                    .HasColumnName("currencyPair")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeFromTypeId)
                    .HasColumnName("exchangeFromTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ExchangeToTypeId)
                    .HasColumnName("exchangeToTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TradeBcid)
                    .HasColumnName("tradeBCid")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TradePlaceDate).HasColumnName("tradePlaceDate");

                entity.Property(e => e.TradeStatus)
                    .IsRequired()
                    .HasColumnName("tradeStatus")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TradeTypeId)
                    .HasColumnName("tradeTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WasArbitrageSuggestion)
                    .HasColumnName("wasArbitrageSuggestion")
                    .HasColumnType("bit(1)");
            });

            modelBuilder.Entity<TradeLog>(entity =>
            {
                entity.HasKey(e => e.TradeId);

                entity.ToTable("trade_log", "ticketing_db");

                entity.Property(e => e.TradeId)
                    .HasColumnName("tradeID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(22,18)");

                entity.Property(e => e.CurrencyPair)
                    .IsRequired()
                    .HasColumnName("currencyPair")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeFromTypeId)
                    .HasColumnName("exchangeFromTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ExchangeToTypeId)
                    .HasColumnName("exchangeToTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("decimal(22,18)");

                entity.Property(e => e.Socialtrade)
                    .HasColumnName("socialtrade")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Socialtradetraderid)
                    .HasColumnName("socialtradetraderid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TradeBcid)
                    .HasColumnName("tradeBCid")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.TradePlaceDate).HasColumnName("tradePlaceDate");

                entity.Property(e => e.TradeStatus)
                    .IsRequired()
                    .HasColumnName("tradeStatus")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TradeTypeId)
                    .HasColumnName("tradeTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TraderUid)
                    .HasColumnName("traderUID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WasArbitrageSuggestion)
                    .HasColumnName("wasArbitrageSuggestion")
                    .HasColumnType("bit(1)");
            });

            modelBuilder.Entity<TradeType>(entity =>
            {
                entity.ToTable("trade_type", "ticketing_db");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserActionType>(entity =>
            {
                entity.ToTable("user_action_type", "ticketing_db");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserActivityLog>(entity =>
            {
                entity.ToTable("user_activity_log", "ticketing_db");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ActionDate)
                    .HasColumnName("actionDate")
                    .HasColumnType("datetime(6)");

                entity.Property(e => e.ActionTypeId)
                    .HasColumnName("actionTypeId")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IpAddress)
                    .HasColumnName("ipAddress")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<UserDetails>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("user_details", "ticketing_db");

                entity.HasIndex(e => e.Emailaddress)
                    .HasName("emailaddress_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Phonenumber)
                    .HasName("phonenumber_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Userid)
                    .HasName("userid_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AccType)
                    .HasColumnName("acc_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Countrycode)
                    .HasColumnName("countrycode")
                    .HasColumnType("int(5)");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.DtCreated)
                    .HasColumnName("dt_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DtModified).HasColumnName("dt_modified");

                entity.Property(e => e.Emailaddress)
                    .HasColumnName("emailaddress")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Housenumber)
                    .HasColumnName("housenumber")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Languageid)
                    .HasColumnName("languageid")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Phonenumber)
                    .HasColumnName("phonenumber")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Referralcode)
                    .HasColumnName("referralcode")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasColumnType("int(11)");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Streetname1)
                    .HasColumnName("streetname1")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Streetname2)
                    .HasColumnName("streetname2")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Streetname3)
                    .HasColumnName("streetname3")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Territoryid)
                    .HasColumnName("territoryid")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<UserDocs>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("user_docs", "ticketing_db");

                entity.HasIndex(e => e.Userid)
                    .HasName("docs_userid_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.DtCreated)
                    .HasColumnName("dt_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DtModified).HasColumnName("dt_modified");

                entity.Property(e => e.Idproofdoc)
                    .HasColumnName("idproofdoc")
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Profilepic)
                    .HasColumnName("profilepic")
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.Property(e => e.Residencedoc)
                    .HasColumnName("residencedoc")
                    .HasMaxLength(512)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserDocs)
                    .HasPrincipalKey<UserDetails>(p => p.Id)
                    .HasForeignKey<UserDocs>(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_docs");
            });

            modelBuilder.Entity<UserNotes>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("user_notes", "ticketing_db");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Createdby)
                    .HasColumnName("createdby")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DtCreated)
                    .HasColumnName("dt_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DtModified).HasColumnName("dt_modified");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Modifiedby)
                    .HasColumnName("modifiedby")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserPwd>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("user_pwd", "ticketing_db");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.DtCreated)
                    .HasColumnName("dt_created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DtModified).HasColumnName("dt_modified");

                entity.Property(e => e.Userpwd1)
                    .IsRequired()
                    .HasColumnName("userpwd")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserPwd)
                    .HasPrincipalKey<UserDetails>(p => p.Id)
                    .HasForeignKey<UserPwd>(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_pwd");
            });

            modelBuilder.Entity<UserVerification>(entity =>
            {
                entity.HasKey(e => e.Userid);

                entity.ToTable("user_verification", "ticketing_db");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdProofFile)
                    .HasColumnName("idProofFile")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IdProofVerified)
                    .HasColumnName("idProofVerified")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ResidenceProofFile)
                    .HasColumnName("residenceProofFile")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ResidenceProofVerified)
                    .HasColumnName("residenceProofVerified")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.VerificationCount)
                    .HasColumnName("verificationCount")
                    .HasColumnType("int(11)");
            });
        }
    }
}
