using UnityEngine;

namespace CommandPattern
{
    // ========= The parent class ========= 
    public abstract class Command
    {
        //How far should the object move after pressing a button
        protected float moveDistance = 1f;

        /// <summary>
        /// Called when we press a key
        /// </summary>
        /// <param name="objTrans">Object to move</param>
        public abstract void Execute(Transform objTrans);

        /// <summary>
        /// Move the object
        /// </summary>
        /// <param name="objTrans">Object to move</param>
        public virtual void Move(Transform objTrans) { }
    }


    // ========= Child classes ========= 

    public class MoveUp : Command
    {
        public override void Execute(Transform objTrans)
        {
            Move(objTrans);
        }

        public override void Move(Transform objTrans)
        {
            objTrans.Translate(objTrans.up * moveDistance);
        }
    }


    public class MoveDown : Command
    {
        public override void Execute(Transform objTrans)
        {
            Move(objTrans);
        }

        public override void Move(Transform objTrans)
        {
            objTrans.Translate(-objTrans.up * moveDistance);
        }
    }


    public class MoveLeft : Command
    {
        public override void Execute(Transform objTrans)
        {
            Move(objTrans);
        }

        public override void Move(Transform objTrans)
        {
            objTrans.Translate(-objTrans.right * moveDistance);
        }
    }


    public class MoveRight : Command
    {
        public override void Execute(Transform objTrans)
        {
            Move(objTrans);
        }

        public override void Move(Transform objTrans)
        {
            objTrans.Translate(objTrans.right * moveDistance);
        }
    }


    public class Wait : Command
    {
        public override void Execute(Transform objTrans)
        {
            //Do nothing
        }
    }
}