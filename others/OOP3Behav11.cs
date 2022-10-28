// File: "OOP3Behav11"
using PT4;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class State
        {
            public abstract void InsertCoin();
            public abstract void GetBall();
            public abstract void ReturnCoin();
            public abstract void AddBall();
        }


        public class ReadyState : State
        {
            public BallMachine _machine;
            public ReadyState(BallMachine machine)
            {
                _machine = machine;
            }
            public override void AddBall()
            {

            }

            public override void GetBall()
            {
                Put("You need to pay first");
            }

            public override void InsertCoin()
            {
                Put("Coin is inserted");
                _machine.SetState(_machine.GetHasPayedState());

            }

            public override void ReturnCoin()
            {
                Put("You need to pay first");
            }
        }

        public class HasPayedState : State
        {

            BallMachine _machine;

            public HasPayedState(BallMachine machine)
            {
                _machine = machine;
            }
            public override void AddBall()
            {

            }

            public override void GetBall()
            {
                Put("Take your ball");
                int count = _machine.DecreaseBallCount();
                if (count > 0)
                {
                    _machine.SetState(_machine.GetReadyState());
                }
                else
                {
                    _machine.SetState(_machine.GetNoBallState());
                }
            }

            public override void InsertCoin()
            {
                Put("You have already paid");
            }

            public override void ReturnCoin()
            {
                Put("Take your coin");
                _machine.SetState(_machine.GetReadyState());
            }
        }

        public class NoBallState : State
        {
            BallMachine _machine;

            public NoBallState(BallMachine machine)
            {
                _machine = machine;
            }

            public override void AddBall()
            {
                _machine.SetState(_machine.GetReadyState());
            }

            public override void GetBall()
            {
                Put("Sorry, balls are over");
            }

            public override void InsertCoin()
            {
                Put("Sorry, balls are over");
            }

            public override void ReturnCoin()
            {
                Put("Sorry, balls are over");
            }
        }


        public class BallMachine : State
        {
            public int ballCount;
            private State ready;
            private State hasPayed;
            private State noBall;
            public State currentState;

            public BallMachine()
            {
                ballCount = 3;
                ready = new ReadyState(this);
                hasPayed = new HasPayedState(this);
                noBall = new NoBallState(this);
                currentState = ready;
            }

            public override void AddBall()
            {
                // currentState.AddBall();
                ballCount++;
                Put("Ball is added");
                Show("CountBall " + ballCount);
                if (currentState == noBall)
                {
                    currentState.AddBall();
                }

            }

            public override void GetBall()
            {
                currentState.GetBall();
            }

            public override void InsertCoin()
            {
                currentState.InsertCoin();
            }

            public override void ReturnCoin()
            {
                currentState.ReturnCoin();
            }

            public int DecreaseBallCount()
            {
                ballCount--;
                Show("CountBall " + ballCount);
                return ballCount;
            }

            public void SetState(State newState)
            {
                currentState = newState;
            }

            public State GetReadyState()
            {
                return ready;
            }
            public State GetHasPayedState()
            {
                return hasPayed;
            }
            public State GetNoBallState()
            {
                return noBall;
            }

        }
        // Implement the ReadyState, HasPayedState
        //   and NoBallState descendant classes

        // Implement the BallMachine class

        public static void Solve()
        {
            Task("OOP3Behav11");
            string str = GetString();
            BallMachine ballMachine = new BallMachine();
            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case 'A':
                        ballMachine.AddBall();
                        Show("A" + i + " state=" + ballMachine.currentState.ToString());
                        break;
                    case 'I':
                        ballMachine.InsertCoin();
                        Show("I" + i + " state=" + ballMachine.currentState.ToString());
                        break;
                    case 'G':
                        ballMachine.GetBall();
                        Show("G" + i + " state=" + ballMachine.currentState.ToString());
                        break;
                    case 'R':
                        ballMachine.ReturnCoin();
                        Show("R" + i + " state=" + ballMachine.currentState.ToString());
                        break;
                }
            }
        }
    }
}
