
const studentMarks = [78, 85, 92, 67, 84];

const calculateTotal = (marks) =>
  marks.reduce((acc, mark) => acc + mark, 0);

const calculateAverage = (marks) =>
  calculateTotal(marks) / marks.length;

const addGraceMarks = (marks, grace = 0) =>
  marks.map(mark => mark + grace);

const analyzeMarks = (marks) => {
  const total = calculateTotal(marks);
  const average = calculateAverage(marks);
  const result = average >= 40 ? "Pass" : "Fail";

  console.log(`
Student Marks Analysis
-------------------------
Marks: ${marks.join(", ")}
Total: ${total}
Average: ${average.toFixed(2)}
Result: ${result}
`);
};

analyzeMarks(studentMarks);