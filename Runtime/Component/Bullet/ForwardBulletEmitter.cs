namespace UnityGameLib.Component.Bullet
{
    public class ForwardBulletEmitter : BulletEmitter
    {
        protected override void ConfigBullet(BulletComponent bullet)
        {
            bullet.Direction = transform.up;
        }
    }
}