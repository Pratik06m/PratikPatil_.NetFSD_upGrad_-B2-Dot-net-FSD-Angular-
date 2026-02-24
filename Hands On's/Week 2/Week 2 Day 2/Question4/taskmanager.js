let tasks = [];

const simulateAsync = (operation, callback) => {
  setTimeout(() => {
    operation();
    callback();
  }, 1000);
};


export const addTaskCallback = (task, callback) => {
  simulateAsync(() => {
    tasks.push(task);
  }, () => {
    console.log(`✅ Task added (Callback): ${task}`);
    callback();
  });
};

export const deleteTaskCallback = (task, callback) => {
  simulateAsync(() => {
    tasks = tasks.filter(t => t !== task);
  }, () => {
    console.log(`🗑 Task deleted (Callback): ${task}`);
    callback();
  });
};

export const listTasksCallback = (callback) => {
  simulateAsync(() => {}, () => {
    console.log(`
📋 Task List (Callback Version)
-------------------------------
${tasks.map((t, i) => `${i + 1}. ${t}`).join("\n")}
`);
    callback();
  });
};


export const addTaskPromise = (task) => {
  return new Promise((resolve) => {
    setTimeout(() => {
      tasks.push(task);
      resolve(`✅ Task added (Promise): ${task}`);
    }, 1000);
  });
};

export const deleteTaskPromise = (task) => {
  return new Promise((resolve) => {
    setTimeout(() => {
      tasks = tasks.filter(t => t !== task);
      resolve(`🗑 Task deleted (Promise): ${task}`);
    }, 1000);
  });
};

export const listTasksPromise = () => {
  return new Promise((resolve) => {
    setTimeout(() => {
      resolve(`
📋 Task List (Promise Version)
-------------------------------
${tasks.map((t, i) => `${i + 1}. ${t}`).join("\n")}
`);
    }, 1000);
  });
};

export const addTaskAsync = async (task) => {
  const message = await addTaskPromise(task);
  console.log(message);
};

export const deleteTaskAsync = async (task) => {
  const message = await deleteTaskPromise(task);
  console.log(message);
};

export const listTasksAsync = async () => {
  const message = await listTasksPromise();
  console.log(message);
};