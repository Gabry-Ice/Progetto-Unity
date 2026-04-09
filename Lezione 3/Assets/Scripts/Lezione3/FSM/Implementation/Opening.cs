using UnityEngine;

namespace MiciomaXD
{
    public class Opening : IBasicState
    {
        private FSMContext ctx;

        private Quaternion toReach;

        public Opening(FSMContext ctx)
        {
            this.ctx = ctx;
        }

        public void BeginState()
        {
            Vector3 toPlayer = (ctx.player.position - ctx.doorPivot.position).normalized; //direction player->door
            float dot = Vector3.Dot(ctx.doorPivot.forward, toPlayer); //dot product between the forward of the door and the direction player->door, if it's positive the player->door direction is the same of the door forward (blue axis), need to rotate clockwise, otherwise need to rotate counterclockwise

            float direction = dot > 0 ? -1f : 1f;

            toReach = ctx.doorPivot.rotation * Quaternion.Euler(0, ctx.angleAperture * direction, 0); //the target rotation is the current rotation of the door multiplied by a rotation of angleAperture degrees in the right direction (we rotate a direction - door's forward direction = doorPivot.rotation - multiplying it by a quaternion)

            ctx.doorAudioSource.clip = ctx.openDoorSFX;
            ctx.doorAudioSource.Play();
        }

        public void EndState()
        {
        }


        public void UpdateState()
        {
            float fractionOfAngleCovered = Quaternion.Angle(ctx.doorPivot.rotation, toReach) / ctx.angleAperture; //at what point of the rotation we are, 0 at the beginning, 1 when we are at the target rotation, this is used to evaluate the movement curve
            ctx.doorPivot.rotation = Quaternion.RotateTowards(
                ctx.doorPivot.rotation,
                toReach,
                ctx.openingSpeed * Time.deltaTime * ctx.movementCurve.Evaluate(fractionOfAngleCovered) //ease-in and out the opening of the door using a curve, we evaluate the curve with the fraction of angle covered, so at the beginning and at the end of the opening the speed will be lower than in the middle
                );

            //transition condition: if the angle between the current rotation of the door and the target rotation is less than 0.1 degrees, we consider the door opened and we switch to the Opened state
            if (Quaternion.Angle(ctx.doorPivot.rotation, toReach) < 0.1f)
            {
                ctx.doorPivot.rotation = toReach; //clamp the rotation to the target rotation to avoid overshooting
                ctx.fsm.SwitchState(new Opened(ctx));
            }

        }
    }
}