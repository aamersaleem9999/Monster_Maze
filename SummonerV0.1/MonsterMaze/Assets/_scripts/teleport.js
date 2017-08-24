var target :Transform;

function OnTriggerEnter (col : Collider)
{
if(col.gameObject.tag=="teleport")
{
  yield WaitForSeconds(2);
  this.transform.position= target.position;
}
}
