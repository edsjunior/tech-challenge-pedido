﻿namespace G64.ProdutoApi.Models;

public class Ingrediente
{
    public Guid Id { get; set; }
    public string? Descricao { get;  set; }
    
    /*
	public Ingrediente(Guid id, string descricao)
    {
        Id = id;
        Descricao = descricao;
    }
    
    public static Ingrediente CriaIngrediente(Guid id, string descricao)
    {
        ValidaDescricao(descricao);
        return new Ingrediente(id, descricao);
    }

    private static void ValidaDescricao(string descricao)
    {
        if (string.IsNullOrEmpty(descricao))
        {
            throw new ArgumentException("Descricao deve estar preenchida");
        }
    }*/
    
}