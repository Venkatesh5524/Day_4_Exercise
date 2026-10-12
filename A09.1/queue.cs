class TQueue<T> {
   public void Enqueue (T a) {
      if (mPos == mData.Length && mFree == 0) {
         var temp = new T[mData.Length * 2];
         for (int i = 0; i < mPos; i++) temp[i] = mData[i];
         mData = temp;
      } else if (mPos == mData.Length && mFree > 0) {
         for (int i = 0; i < mPos - mFree; i++)
            mData[i] = mData[i + mFree];
         mPos -= mFree;
         mFree = 0;
      }
      mData[mPos++] = a;
   }

   public T Dequeue () {
      if (mPos == mFree) throw new Exception ("Queue Empty");
      T result = mData[mFree];
      mData[mFree++] = default!;
      return result;
   }

   public bool IsEmpty => mPos == mFree;

   T[] mData = new T[4];
   int mPos = 0;
   int mFree = 0;
}