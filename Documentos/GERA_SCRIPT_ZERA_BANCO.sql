
/*
01 - DISABLE TRIGGER
02 - TRUNCATE TABLE
03 - DELETE
04 - ENABLE TRIGGER
05 - COPIA TABELAS 
*/
Set Nocount On 


--------------------------------------------------------------------------------------------------
---Gera script Disable Trigger
--------------------------------------------------------------------------------------------------
Select 'Alter Table ' + Name + ' Disable Trigger All ' + char(10) + 'GO'   From Sysobjects where xtype = 'u'

--------------------------------------------------------------------------------------------------
---Gera script Disable Constraint
--------------------------------------------------------------------------------------------------
SELECT 'Alter TABLE ' + NAME  + ' NOCHECK CONSTRAINT ALL ' + CHAR(10) + 'GO'  FROM SYSOBJECTS WHERE XTYPE = 'U'


--------------------------------------------------------------------------------------------------
---Gera script Truncate Table 
--------------------------------------------------------------------------------------------------
Select 'Truncate Table ' + Name + char(10) + 'GO'   From Sysobjects where xtype = 'u' where xtype = 'u'
And	Name Not In (	'Usuario',
							'Usuario_Senha',	 
							'Funcao',
							'Usuario_Funcao',
							'Usuario_Empresa', 
							'Terceiro',
							'Terceiro_Endereco',
							'Terceiro_Complementar',
							'Empresa',
							'Municipio',
							'produto',
							'funcao',
							'Parametro',
							'Parametro_Valor', 
							'operacao_protheus',
							'Qualidade',
							'Logradouro',
							'NUMERACAO', 	
							'XEVENTO',
							'Bv_Evento',
							'Tipo_Comercial',
							'Bv_Funcao',
							'Composicao_Relatorio',
							'Historico',
							'CondicaoPagto',
							'Motivo_Valoracao',
							'Motivo_Falha',
							'Produto_Protheus',
							'uf' ,
							'Genero',
							'Tipo_Midia',
							'Motivo_Alteracao',
							'Critica_Protheus_Mensagem',
							'Terceiro_Funcao',
							'Web_Status',
							'Classe_Potencia',
							'WEB_Parametro_Secundagem',
							'Critica_Mensagem',
							'Motivo_Cancelamento',
							'Rede',
							'Para_Desconto',
							'Categoria_Cliente',
							'Carac_Veiculacao',
							'Formulario_Protheus',
							'MGI_Integracao_Processo',
							'Log_Usuario_Operacao',
							'Mgi_Fechamento',
							'Caracteristica_Contrato',
							'Faixa_Horaria',
							'Tipo_Intermediario',
							'Forma_Tributacao',
							'Forma_Pgto',
							'Composicao_Cliente_Produto',
							'Log_Negociacao_Operacao'							'
						)

--------------------------------------------------------------------------------------------------
---Gera script Delete Table 
--------------------------------------------------------------------------------------------------
Select 'Delete From  ' + Name + char(10) + 'GO'   From Sysobjects where xtype = 'u'
And	Name Not Like '%Tb_Proposta%'
And	Name Not In (	'Usuario',
							'Usuario_Senha',	 
							'Funcao',
							'Usuario_Funcao',
							'Usuario_Empresa', 
							'Terceiro',
							'Terceiro_Endereco',
							'Terceiro_Complementar',
							'Empresa',
							'Municipio',
							'produto',
							'funcao',
							'Parametro',
							'Parametro_Valor', 
							'operacao_protheus',
							'Qualidade',
							'Logradouro',
							'NUMERACAO', 	
							'XEVENTO',
							'Bv_Evento',
							'Tipo_Comercial',
							'Bv_Funcao',
							'Composicao_Relatorio',
							'Historico',
							'CondicaoPagto',
							'Motivo_Valoracao',
							'Motivo_Falha',
							'Produto_Protheus',
							'uf' ,
							'Genero',
							'Tipo_Midia',
							'Motivo_Alteracao',
							'Critica_Protheus_Mensagem',
							'Terceiro_Funcao',
							'Web_Status',
							'Classe_Potencia',
							'WEB_Parametro_Secundagem',
							'Critica_Mensagem',
							'Motivo_Cancelamento',
							'Rede',
							'Para_Desconto',
							'Categoria_Cliente',
							'Carac_Veiculacao',
							'Formulario_Protheus',
							'MGI_Integracao_Processo',
							'Log_Usuario_Operacao',
							'Mgi_Fechamento',
							'Caracteristica_Contrato',
							'Faixa_Horaria',
							'Tipo_Intermediario',
							'Forma_Tributacao',
							'Forma_Pgto',
							'Composicao_Cliente_Produto',
							'Log_Negociacao_Operacao'							'
						)

--------------------------------------------------------------------------------------------------
---Gera script Enable Constraint
--------------------------------------------------------------------------------------------------
SELECT 'Alter TABLE ' + NAME  + ' CHECK CONSTRAINT ALL   ' + CHAR(10) + 'GO'  FROM SYSOBJECTS WHERE XTYPE = 'U'

--------------------------------------------------------------------------------------------------
---Enabled Triiger
--------------------------------------------------------------------------------------------------
Select 'Alter Table ' + Name + ' Enable Trigger All ' + char(10) + 'GO'   From Sysobjects where xtype = 'u'