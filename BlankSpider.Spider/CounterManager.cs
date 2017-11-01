using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Spider
{
    public class CounterManager
    {
        private int _COUNT_NUMBER_ = 0;
        private int _TRYING_COUNT_ = 0;
        private int _MAX_TRYING_COUNT_ = 100;
        private int _CURRENT_NUMBER_ = 0;
        private readonly object _NEED_LOCK_OBJECT_ = new object();
        private readonly object _NEED_LOCK_OBJECT_TRYING_COUNT_ = new object();

        public CounterManager() { }
        public CounterManager(int START_NUMBER_COUNT) {
            _COUNT_NUMBER_ = START_NUMBER_COUNT;
        }

        public CounterManager(int START_NUMBER_COUNT, int MAX_TRYING_COUNT_)
        {
            _COUNT_NUMBER_ = START_NUMBER_COUNT;
            _MAX_TRYING_COUNT_ = MAX_TRYING_COUNT_;
        }

        public int GetNumber() {
            lock (_NEED_LOCK_OBJECT_) {
                return _COUNT_NUMBER_++;
            }
        }

        public void Reset(int START_NUMBER_COUNT, int MAX_TRYING_COUNT_)
        {
            lock (_NEED_LOCK_OBJECT_) {
                _COUNT_NUMBER_ = START_NUMBER_COUNT;
                _MAX_TRYING_COUNT_ = MAX_TRYING_COUNT_;
                _TRYING_COUNT_ = 0;
            }
        }

        public int StartNumber {
            get {
                lock (_NEED_LOCK_OBJECT_) {
                    return _COUNT_NUMBER_;
                }
            }
            set {
                lock (_NEED_LOCK_OBJECT_){
                    _COUNT_NUMBER_ = value;
                }
            }
        }

        public bool InCreaseTryingCount() {
            lock (_NEED_LOCK_OBJECT_TRYING_COUNT_) {
                _TRYING_COUNT_++;
                return (_TRYING_COUNT_ <= _MAX_TRYING_COUNT_);
            }
        }

        public void ResetTryingCount()
        {
            lock (_NEED_LOCK_OBJECT_TRYING_COUNT_)
            {
                _TRYING_COUNT_= 0;
            }
        }

        public int CurrentNumber {
            get {
                lock (_NEED_LOCK_OBJECT_) {
                    return _CURRENT_NUMBER_;
                }
            }
            set {
                lock (_NEED_LOCK_OBJECT_) {
                    _CURRENT_NUMBER_ = value;
                    _COUNT_NUMBER_ = 0;

                }
                
            }
        }

        public int CurrentTryingCount
        {
            get
            {
                lock (_NEED_LOCK_OBJECT_TRYING_COUNT_)
                {
                    return _TRYING_COUNT_;
                }
            }
            set
            {
                lock (_NEED_LOCK_OBJECT_TRYING_COUNT_)
                {
                    _TRYING_COUNT_ = value;
                }
            }
        }





    }
}
