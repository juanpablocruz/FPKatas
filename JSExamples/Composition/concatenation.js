const objs = [
    { a: 'a', b: 'ab' },
    { b: 'b' },
    { c: 'c', b: 'cb' }
  ];
const concatenate = (a, o) => ({...a, ...o});
const c = objs.reduce(concatenate, {});
console.log(
  'concatenation',
  c,
  `enumerable keys: ${ Object.keys(c) }`
);
