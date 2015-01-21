namespace Race.src
{
    class PlayerType
    {
        internal int type;
        internal int speedBonus;
        internal int accelerationBonus;
        internal int agilityBonus;
        internal int armorBonus;
        internal int shieldPowerBonus;
        internal int shieldRechargeBonus;
        internal int suspensionBonus;
        internal int tiresBonus;
        internal int downforceBonus;

        public PlayerType(int type, int speedBonus, int accelerationBonus, int agilityBonus, int armorBonus, int shieldPowerBonus, int shieldRechargeBonus, int suspensionBonus, int tiresBonus, int downforceBonus)
        {
            //member initialization
            this.type = type;
            this.speedBonus = speedBonus;
            this.accelerationBonus = accelerationBonus;
            this.agilityBonus = agilityBonus;
            this.armorBonus = armorBonus;
            this.shieldPowerBonus = shieldPowerBonus;
            this.shieldRechargeBonus = shieldRechargeBonus;
            this.suspensionBonus = suspensionBonus;
            this.tiresBonus = tiresBonus;
            this.downforceBonus = downforceBonus;
        }
    }
}
