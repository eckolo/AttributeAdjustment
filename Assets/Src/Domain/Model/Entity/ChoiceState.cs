using Assets.Src.Controller;
using Assets.Src.Domain.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Src.Domain.Model.Entity
{
    public class ChoiceState : ViewStateAbst, IViewKey, IDisposable
    {
        public ChoiceState(List<string> choiceList, int? choiced)
        {
            this.choiceList = choiceList;
            this.choiced = choiced;
        }

        /// <summary>
        /// 選択肢リスト
        /// </summary>
        public List<string> choiceList { get; }
        /// <summary>
        /// 現在選択中のインデックス
        /// nullであればキャンセル
        /// </summary>
        public int? choiced
        {
            get => _choiced;
            set {
                if(value is int valueInt && (choiceList?.Any() ?? false))
                {
                    while(valueInt < 0)
                        valueInt += choiceList.Count;
                    _choiced = valueInt % choiceList.Count;
                }
                else
                {
                    _choiced = null;
                }
            }
        }
        int? _choiced = 0;
        /// <summary>
        /// ↑キー押下状態継続時間
        /// </summary>
        public int keepUpTime { get; set; } = 0;
        /// <summary>
        /// ↓キー押下状態継続時間
        /// </summary>
        public int keepDownTime { get; set; } = 0;
        /// <summary>
        /// 選択処理完了フラグ
        /// </summary>
        public bool isFinished
        {
            get => _isFinished || choiceList is null || !choiceList.Any();
            set => _isFinished = value;
        }
        bool _isFinished = false;

        public async void Dispose() => await this.End();
    }
}
