﻿using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace Vampire
{
    public class Building_Coffin : Building_Sarcophagus
    {
        // RimWorld.Building_Grave
#pragma warning disable 169
        private Graphic cachedGraphicFull;
#pragma warning restore 169


        //public override void Draw()

        //public override bool Accepts(Thing thing)

        private int CorpseCapacity => this.def.size.x;
        private int CorpsesLoaded => innerContainer.Count;
        public override bool Accepts(Thing thing)
        {
            return innerContainer.CanAcceptAnyOf(thing) && CorpsesLoaded < CorpseCapacity && GetStoreSettings().AllowedToAccept(thing);
        }

        // RimWorld.Building_Grave
        //public override Graphic Graphic

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo g in base.GetGizmos())
                yield return g;

            var p = Corpse?.InnerPawn ?? (Pawn)(innerContainer.FirstOrDefault()) ?? null;
            if ((p?.IsVampire() ?? false) || (p?.HasVampireHediffs() ?? false))
            {
                foreach (Gizmo y in HarmonyPatches.GraveGizmoGetter(p, this))
                    yield return y;
            }
        }


        //public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn selPawn)


    }
}
