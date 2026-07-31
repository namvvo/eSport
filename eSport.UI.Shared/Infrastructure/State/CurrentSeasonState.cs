
using eSport.UI.Shared.Models.Data;

namespace eSport.UI.Shared.Infrastructure.State
{
    public class CurrentSeasonState : BaseState<CurrentSeasonModel>
    {
        public CurrentSeasonState() : base(
            new CurrentSeasonModel()
            { 
                
            })
        {
        }
        public CurrentSeasonModel Value
        {
            get => State;
           
        }
        public void Reset()
        {

            State= new CurrentSeasonModel();
            Update(State);

        }
        public void UpdateState(CurrentSeasonModel currentSeason)
        {
            
            Update(currentSeason);


        }
    }
}
