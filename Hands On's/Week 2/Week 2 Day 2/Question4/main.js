
import {
  addTaskCallback,
  deleteTaskCallback,
  listTasksCallback,
  addTaskPromise,
  deleteTaskPromise,
  listTasksPromise,
  addTaskAsync,
  deleteTaskAsync,
  listTasksAsync
} from "./taskmanager.js";


addTaskCallback("Learn JavaScript", () => {
  addTaskCallback("Practice Async", () => {
    listTasksCallback(() => {
      deleteTaskCallback("Learn JavaScript", () => {
        listTasksCallback(() => {
          console.log("----- Callback Version Complete -----\n");
        });
      });
    });
  });
});


addTaskPromise("Read Book")
  .then(console.log)
  .then(() => addTaskPromise("Write Code"))
  .then(console.log)
  .then(() => listTasksPromise())
  .then(console.log)
  .then(() => deleteTaskPromise("Read Book"))
  .then(console.log)
  .then(() => listTasksPromise())
  .then(console.log)
  .then(() => {
    console.log("----- Promise Version Complete -----\n");
  });


const runAsyncVersion = async () => {
  await addTaskAsync("Workout");
  await addTaskAsync("Meditate");
  await listTasksAsync();
  await deleteTaskAsync("Workout");
  await listTasksAsync();
  console.log("----- Async/Await Version Complete -----");
};

runAsyncVersion();