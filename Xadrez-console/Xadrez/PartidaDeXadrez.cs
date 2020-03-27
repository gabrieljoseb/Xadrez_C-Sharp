using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino) 
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
                Capturadas.Add(pecaCapturada);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.Peca(pos) == null)
                throw new DomainException("Não existe peça na posição de origem escolhida!");
            if (JogadorAtual != Tab.Peca(pos).Cor)
                throw new DomainException($"Você deve escolher uma peça {JogadorAtual.ToString().ToUpper()}");
            if (!Tab.Peca(pos).ExisteMovimentosPossiveis())
                throw new DomainException("A peça escolhida não possui movimentos possiveis!");
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).PodeMoverPara(destino))            
                throw new DomainException("A posição de destino é inválida!");            
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
                JogadorAtual = Cor.Preta;
            else
                JogadorAtual = Cor.Branca;
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca x in Capturadas)            
                if (x.Cor == cor)
                    aux.Add(x);
            return aux;            
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca x in Pecas)
                if (x.Cor == cor)
                    aux.Add(x);

            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, (new Torre(Tab, Cor.Branca)));
            ColocarNovaPeca('b', 1, (new Torre(Tab, Cor.Branca)));
            ColocarNovaPeca('c', 1, (new Torre(Tab, Cor.Branca)));
            ColocarNovaPeca('d', 1, (new Torre(Tab, Cor.Branca)));
            ColocarNovaPeca('e', 1, (new Torre(Tab, Cor.Branca)));
            ColocarNovaPeca('f', 1, (new Torre(Tab, Cor.Branca)));
            
            ColocarNovaPeca('a', 8, (new Rei(Tab, Cor.Preta)));
            ColocarNovaPeca('b', 8, (new Rei(Tab, Cor.Preta)));
            ColocarNovaPeca('c', 8, (new Rei(Tab, Cor.Preta)));
            ColocarNovaPeca('d', 8, (new Rei(Tab, Cor.Preta)));
            ColocarNovaPeca('e', 8, (new Rei(Tab, Cor.Preta)));
            ColocarNovaPeca('f', 8, (new Rei(Tab, Cor.Preta)));
        }
    }
}
