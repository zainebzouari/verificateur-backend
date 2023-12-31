﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace verificateur.Models;

public partial class Article
{
    public string IdArticle { get; set; } = null!;


    public DateTime? DDPromotion { get; set; }

    public string? DesignationArticle { get; set; }

    public DateTime? DFPromotion { get; set; }

    public string IdFamille { get; set; } 

    public string? IdMarque { get; set; }

    public string? IdStation { get; set; }

    public byte[]? ImageArt { get; set; }

    public decimal? PrixTTC { get; set; }

    public decimal? TauxPromotion { get; set; }

    public decimal? PrixPromotion { get; set; }

    public virtual ICollection<CBFil> CBFils { get; set; } = new List<CBFil>();

    public virtual Famille IdFamilleNavigation { get; set; }

    public virtual Marque IdMarqueNavigation { get; set; }

    public virtual Station IdStationNavigation { get; set; }
}