class TQueue<T> {
   // Adds an element to the rear of the queue
   public void Enqueue (T value) {
      if (mCount == mData.Length) Resize ();
      mData[mPos] = value;
      mPos = (mPos + 1) % mData.Length;
      mCount++;
   }

   // Removes and returns the element from the front of the queue
   public T Dequeue () {
      if (IsEmpty) throw new InvalidOperationException ("Queue Empty");
      T value = mData[mFree];
      mData[mFree] = default!;
      mFree = (mFree + 1) % mData.Length;
      mCount--;
      return value;
   }

   // Checks whether the queue contains no elements
   public bool IsEmpty => mCount == 0;

   // Doubles the queue capacity and rearranges the elements in queue order
   void Resize () {
      T[] temp = new T[mData.Length * 2];
      for (int i = 0; i < mCount; i++) temp[i] = mData[(mFree + i) % mData.Length];
      mData = temp;
      mFree = 0;
      mPos = mCount;
   }

   T[] mData = new T[4];
   int mPos = 0;
   int mFree = 0;
   int mCount = 0;
}