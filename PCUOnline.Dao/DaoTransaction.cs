using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;

namespace PCUOnline.Dao
{
    public sealed class DaoTransaction : IDisposable
    {
        private enum State
        {
            None , 
            Begin ,
            Commit , 
            Rollback
        }
        private static int id = 0;

        private State state = State.None;
        private IDbTransaction tx;
        private IDbConnection conn;
        private bool requiredCommit = true;

        //Constructor
        public DaoTransaction(IDbConnection c) : this(c,true)
        {
            
        }

        public DaoTransaction(IDbConnection c, bool isRequiredCommit)
        {
            requiredCommit = isRequiredCommit;
            this.conn = c;
            this.Open();
        }
        //Properties

        public IDbConnection Connection
        {
            get { return conn; }
        }
        public IDbTransaction Transaction
        {
            get { return tx; }
        }
        public bool IsEnded
        {
            get {
                if (state == State.Begin)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public bool IsRequiredCommit
        {
            get { return requiredCommit; }
        }
        //Methods
        private static void CheckID()
        {
            if (id + 1 == int.MaxValue)
            {
                id = 0;
            }
            else
            {
                id++;
            }
        }
        public void Open()
        {
            //แก้ไข เมื่อวันที่ Oct 24 , 2006
            //this.Open(IsolationLevel.RepeatableRead);
            this.Open(IsolationLevel.RepeatableRead);
        }
        public void Open(IsolationLevel level)
        {
            CheckID();

            if (this.conn != null)
            {
                this.conn.Open();
                if (requiredCommit)
                    this.tx = this.conn.BeginTransaction();
                    //this.tx = this.conn.BeginTransaction(level);

                this.state = State.Begin;
            }
            
            Debug.WriteLine(string.Format("-- Connection {0} is opened. --",id.ToString("00000")));
        }
        public void Commit()
        {
            if (this.tx != null)
            {
                if (requiredCommit)
                {
                    this.tx.Commit();
                    this.state = State.Commit;

                    Debug.WriteLine(string.Format("-- Connection {0} is committed. --", id.ToString("00000")));
                }
                this.Close(this.tx.Connection);
            }
        }
        public void Rollback()
        {
            if (this.tx != null)
            {
                if (requiredCommit)
                {
                    this.tx.Rollback();
                    this.state = State.Rollback;

                    Debug.WriteLine(string.Format("-- Connection {0} is rollback. --", id.ToString("00000")));
                }
                this.Close(this.tx.Connection);
            }
        }
        public void Close()
        {
            Close(this.conn);
        }
        private void Close(IDbConnection c)
        {
            try
            {
                if(conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    this.tx = null;
                    this.state = State.None;

                    Debug.WriteLine(string.Format("-- Connection {0} is closed. --", id.ToString("00000")));
                }
            }
            catch { }
        }

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                this.Close();
                GC.SuppressFinalize(this);
            }
            catch { }
        }

        #endregion
    }
}
