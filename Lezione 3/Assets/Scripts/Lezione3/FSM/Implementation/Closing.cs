using UnityEngine;

namespace MiciomaXD
{
    public class Closing : IBasicState
    {
        private FSMContext ctx;

        Quaternion toReach;
        public Closing(FSMContext ctx)
        {
            this.ctx = ctx;
        }

        public void BeginState()
        {
            Vector3 toPlayer = (ctx.player.position - ctx.doorPivot.position).normalized;
            float dot = Vector3.Dot(ctx.doorPivot.forward, toPlayer);

            float direction = dot > 0 ? -1f : 1f;

            toReach = ctx.doorPivot.rotation * Quaternion.Euler(0, -ctx.angleAperture * direction, 0);

            ctx.doorAudioSource.clip = ctx.openDoorSFX;
            ctx.doorAudioSource.Play();
        }

        public void EndState()
        {
        }


        public void UpdateState()
        {
            float fractionOfAngleCovered = Quaternion.Angle(ctx.doorPivot.rotation, toReach) / ctx.angleAperture;
            ctx.doorPivot.rotation = Quaternion.RotateTowards(
                ctx.doorPivot.rotation,
                toReach,
                ctx.openingSpeed * Time.deltaTime * ctx.movementCurve.Evaluate(fractionOfAngleCovered)
                );

            if (Quaternion.Angle(ctx.doorPivot.rotation, toReach) < 0.1f)
            {
                ctx.doorPivot.rotation = toReach;
                ctx.fsm.SwitchState(new Closed(ctx));
            }
        }
    }
}