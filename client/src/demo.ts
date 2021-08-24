let data: number | string;
data = '42';
data = 42;

const car1: Icar = {
  color: 'blue',
  model: 'bmw',
};

const car2: Icar = {
  color: 'blue',
  model: 'bmw',
  topSpeed: 100,
};

interface Icar {
  color: string;
  model: string;
  topSpeed?: number;
}
