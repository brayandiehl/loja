﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Loja.Telas.Configuracoes.Estrategia
{
    public partial class EstrategiasDinamica : Form
    {
        private string ClasseSelecionada { get; set; }
        public EstrategiasDinamica()
        {
            InitializeComponent();
            Classes.ClassEstrategia.RetornarEstrategias(TabelaClasses);
        }

        private void BtSalvar_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            for (int a = 0; a < TabelaStatus.RowCount; a++)
            {
                if (string.IsNullOrEmpty((string)TabelaStatus.Rows[a].Cells["ValorParametro"].Value))
                {
                    if (MessageBox.Show("Valor do Paramêtro está em branco! \nAo continuar será atribuido atuomáticamente o valor 0!\nDesejá continuar?", "ERRO", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                    {
                        Cursor = Cursors.Default;
                        return;              
                    }
                    else
                    {
                        TabelaStatus[1, a].Value = "0";
                    }
                }
                var ParametroAtual = TabelaStatus.Rows[a].Cells["ValorParametro"].Value.ToString();
                Classes.ClassEstrategia.RetornaValorParametro(ClasseSelecionada, TabelaStatus.Rows[a].Cells["Estrategia"].Value.ToString());
                if (!ParametroAtual.Equals(Classes.ClassEstrategia.Parametro))
                {
                    if (!Classes.ClassEstrategia.AtualizaValorParametro(ClasseSelecionada, TabelaStatus.Rows[a].Cells["Estrategia"].Value.ToString(), ParametroAtual))
                    {
                        if (MessageBox.Show("Erro ao Atualizar\nClasse:  " + ClasseSelecionada + "\nEstratégia: " + TabelaStatus.Rows[a].Cells["Estrategia"].Value.ToString() + "\nParametro: " + ParametroAtual + "\nErro: " + Classes.ClassEstrategia.Erro + "\n\nDEsejá continuar sem atualizar o parametro acima?", "ERRO", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                        {
                            Cursor = Cursors.Default;
                            return;
                        }
                    }
                }
            }
            Cursor = Cursors.Default;
        }

        private void TabelaClasses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ClasseSelecionada = TabelaClasses.Rows[e.RowIndex].Cells["Classe"].Value.ToString();
                Classes.ClassEstrategia.RetornaParametros(TabelaStatus, ClasseSelecionada);
            }
        }
    }
}
