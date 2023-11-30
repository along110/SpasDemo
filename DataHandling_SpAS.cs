if (iPacketCount == 0)
{
  //Detect auxiliary channel (e.g. Checksum)
  if ((iNewData & 0xF0) != 0xF0)
  {
    //Mask high byte data
    iData = ((iNewData & 0x0F) << 8); 
    bAux = false;
  }
  else
    bAux = true;
  iPacketCount++;//go to next packet
}
else if (iPacketCount == 1)
{
  if (!bAux)
  {
    //Add Packet2 data to Packet1 data
    iData = iNewData + iData;
    //Check if 12-bit data is minus
    if ((iData & 0x0800) == 0x0800)
    {
      iData = iData - 0x1000;//set minus integer value
    }
    //Convert into double flow value in l/s
    double dData = Convert.ToDouble(iData) * 0.01d;

    //Display flow value
    SetFlowValue(dData);    
  }
  else
  {
    //ToDo: Checksum Test
  }
  iPacketCount = 0;
}

